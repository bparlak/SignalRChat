using Microsoft.AspNetCore.SignalR;
using SignalRChat.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private static readonly ConcurrentDictionary<string, UserRepository> Users = new ConcurrentDictionary<string, UserRepository>();
        public async Task SendMessage(string user, string message)
        {

            //await Clients.All.SendAsync("ReceiveMessage", user, message);
            await Clients.Others.SendAsync("ReceiveMessageOthers", user, message);
            await Clients.Caller.SendAsync("ReceiveMessageCaller", message);
        }
        public async Task StartConnection(string userName, string groupName)
        {
            UserRepository.AddUser(userName);
            await Clients.All.SendAsync("SendGroup", $"{userName} has joined the group {groupName}.");
            await Clients.All.SendAsync("GetUsers",UserRepository.UserList);
        }
        public async Task StopConnection(string userName, string groupName)
        {
            UserRepository.RemoveUser(userName);
            await Clients.All.SendAsync("SendGroup", $"{userName} has left the group {groupName}.");
            await Clients.All.SendAsync("GetUsers", UserRepository.UserList);
        }

        //public override async Task OnConnectedAsync()
        //{

        //    await Groups.AddToGroupAsync(Context.ConnectionId, "SignalRUsers");
        //    await Clients.Group("SignalRUsers").SendAsync("SendGroup", $"{Context.User.Identity.Name} has joined the group SignalRUsers.");
        //    await base.OnConnectedAsync();
        //}
        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalRUsers");
        //    await Clients.Group("SignalRUsers").SendAsync("SendGroup", $"{Context.ConnectionId} has left the group SignalRUsers.");
        //    await base.OnDisconnectedAsync(exception);
        //}


        //public Task SendPrivateMessage(string user, string message)
        //{
        //    return Clients.User(user).SendAsync("RecievePrivateMessage", message);
        //}
        //public async Task AddToGroup(string groupName,string name)
        //{
        //    await Groups.AddToGroupAsync(Context.ConnectionId, name);
        //    await Clients.Group(groupName).SendAsync("SendGroup", $"{name} has joined the group {groupName}.");
        //}
        //public async Task RemoveFromGroup(string groupName)
        //{
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        //    await Clients.Group(groupName).SendAsync("SendGroup", $"{Context.ConnectionId} has left the group {groupName}.");
        //}
    }
}
