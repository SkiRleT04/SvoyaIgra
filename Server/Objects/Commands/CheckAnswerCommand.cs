using Core.Enums;
using Core.Packets.Request;
using Core.Packets.Response;
using Newtonsoft.Json;
using Server.Objects.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Objects.Commands
{
    class CheckAnswerCommand : ICommand
    {
        public void Excecute(ClientObject client, ServerObject server, RoomObject room, string packet = "")
        {
            Console.WriteLine("Check answer");
            var request = JsonConvert.DeserializeObject<CheckAnswerRequest>(packet);
            var response = new CheckAnswerResponse();

            response.Status = DB.CheckAnswer(request.Question);
            Console.WriteLine($"Answer status: {response.Status }");
            int points;
            if (response.Status == ResponseStatus.Ok)
            {
                points = request.Question.Points;
                room.Selector = client;
                //очищаем клиентов ответивших не верно
                room.Game.RemoveQuestionFromTable(request.Question.Id);
                room.Respondents.Clear();

                NotifyPlayersAboutUpdateRoom(client, room, UpdateRoomType.UpdateTable);

            }
            else
            {
                points = request.Question.Points * -1;
                room.Respondents.Add(client);
                //если все ответили не верно, назначаем селектором первого игрока
                if (room.Clients.Count == room.Respondents.Count)
                {
                    room.Game.RemoveQuestionFromTable(request.Question.Id);
                    if (room.Clients.Count != 0)
                        room.Selector = room.Clients.First();
                    room.Respondents.Clear();
                    NotifyPlayersAboutUpdateRoom(client, room, UpdateRoomType.UpdateTable);
                }

                if (room.Respondents.Count != 0)
                    //обновляем статус кнопки ответа для всех игроков
                    ChangeAnswerButtonPropertyForPlayers(room);

            }

            client.UpdatePoints(points);
            string packetResponse = JsonConvert.SerializeObject(response);
            room.SendMessageToDefiniteClient(packetResponse, client);
            //отправляем уведомление об обновлении счета игрока
            NotifyPlayersAboutUpdateRoom(client, room,UpdateRoomType.UpdatePlayers);

        }

        private void ChangeAnswerButtonPropertyForPlayers(RoomObject room)
        {
            foreach (var client in room.Clients)
            {
                if (!room.Respondents.Contains(client))
                {
                    var response = new BlockAnswerButtonResponse();
                    response.IsEnabled = true;
                    string packetResponse = JsonConvert.SerializeObject(response);
                    room.SendMessageToDefiniteClient(packetResponse, client);
                }
            }
        }

        private void NotifyPlayersAboutUpdateRoom(ClientObject client, RoomObject room, UpdateRoomType type)
        {
            var response = new UpdateRoomResponse();
            if (type == UpdateRoomType.UpdatePlayers)
            {
                response.Type = UpdateRoomType.UpdatePlayers;
                response.Player = client.Player;
                response.Selector = room.Selector.Player;
                if (room.Respondent != null)
                    response.Respondent = room.Respondent.Player;
            }
            else
            {
                response.Type = UpdateRoomType.UpdateTable;
            }
            string packetResponse = JsonConvert.SerializeObject(response);
            //после отправки удаляем респондента
            room.Respondent = null;
            room.SendMessageToAllClients(packetResponse);
        }
    }
}
