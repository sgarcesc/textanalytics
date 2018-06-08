using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TextAnalytics.Hubs
{
    public class File : Hub
    {
        public async Task SendFile(string fileName)
        {
            await Clients.All.SendAsync("ReceiveFile", fileName);
        }
    }
}
