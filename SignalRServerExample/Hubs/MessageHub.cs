using Microsoft.AspNetCore.SignalR;

namespace SignalRServerExample.Hubs
{
	public class MessageHub : Hub
	{
		//public async Task SendMessageAsync(string message, IEnumerable<string> connectionIds) => Group lara kadar bu kullanıldı
		//public async Task SendMessageAsync(string message, string groupName, IEnumerable<string> connectionIds) => Group - GroupExcept
		//public async Task SendMessageAsync(string message, IEnumerable<string> groups) => Groups
		public async Task SendMessageAsync(string message, string groupName)
        {
            #region Caller

            // Sadece server'a bildirim gönderen client'la iletişim kurar.
            //await Clients.Caller.SendAsync("receiveMessage", message);

            #endregion
            #region All

            // Server’a bağlı olan tüm client’larla iletişim kurar.
            //await Clients.All.SendAsync("receiveMessage", message);

            #endregion
            #region Other

            // Sadece server'a bildirim gönderen client dışında Server'a bağlı olan tüm client'lara mesaj gönderir.
            //await Clients.Others.SendAsync("receiveMessage", message);

            #endregion

            #region Hub Clients Metotları
            #region AllExcept
            // Belirtilen client’lar hariç server’a bağlı olan tüm client’lara bildiride bulunur.
            //await Clients.AllExcept(connectionIds).SendAsync("receiveMessage", message);
            #endregion
            #region Client
            // Server'a bağlı olan clientlar arasından sadece belirli client'a bildiride bulunmayı gerçekleştirebilmekteyiz.
            //await Clients.Client(connectionIds.First()).SendAsync("receiveMessage", message);
            #endregion
            #region Clients
            // Server'a bağlı olan clientlar arasından belitilen client'lara bildiride bulunmayı gerçekleştirebilmekteyiz.
            //await Clients.Clients(connectionIds).SendAsync("receiveMessage", message); 
            #endregion
            #region Group
            // Belirtilen gruptaki tüm client'lara bildiride bulunan fonksiyondur.
            // Önce gruplar oluşturulmalı ve ardından client'lar gruplara subsc. olmalıdır.  
            //await Clients.Group(groupName).SendAsync("receiveMessage", message);
            #endregion
            #region GroupExcept
            // Belirtilen gruptaki, belirtilen client'lar dışındaki tüm client'lara mesaj iletmemizi sağlayan fonksiyondur.
            //await Clients.GroupExcept(groupName, connectionIds).SendAsync("receiveMessage", message);
            #endregion
            #region Groups
            // Belirtilen gruptaki, belirtilen client'lar dışındaki tüm client'lara mesaj iletmemizi sağlayan fonksiyondur.
            //await Clients.Groups(groups).SendAsync("receiveMessage", message);
            #endregion
            #region OthersInGroup
            // Bildiride bulunan client haricinde gruptaki tüm clientlara bildiride bulunan fonsksiyondur.
            await Clients.OthersInGroup(groupName).SendAsync("receiveMessage", message);
            #endregion
            #region User
            // Authentication olan kullanıcılarla ilişkili tek bir client’a bildiride bulunmamızı sağlayan fonksiyondur.
            #endregion
            #region Users
            // Authentication olan kullanıcılarla ilişkili tüm client’lara bildiride bulunmamızı sağlayan fonksiyondur.
            #endregion
            #endregion
        }

        public override async Task OnConnectedAsync()
		{
			await Clients.Caller.SendAsync("getConnectionId", Context.ConnectionId);
		}

        public async Task addGroup(string connectionId, string groupName)
        {
            await Groups.AddToGroupAsync(connectionId, groupName); // Girilen groupName adında grup açıp içine gönderilen connectionId kişisi eklendi. Eğer groupName zaten varsa oraya ekleyecektir.
        }
	}
}
