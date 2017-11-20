using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR2D2.Hubs
{
    public class EstimateHub : Hub
    {
        public void SubmitStory(string story)
        {
            EstimateHelper.CurrentStory = story;
            Clients.All.submitStory(story);
        }

        public void SubmitEstimate(string participant, int value)
        {
            EstimateHelper.StoryPoints[Context.ConnectionId] = value;

            if (EstimateHelper.StoryPoints.Count >= EstimateHelper.ConnectedIds.Count)
            {
                var results = EstimateHelper.StoryPoints.Select(x => new { Value = x.Value, Name = EstimateHelper.ConnectedIds[x.Key]}).ToList();

                Clients.All.displayResults(results);
            }
        }

        public void ResetStory()
        {
            EstimateHelper.CurrentStory = null;
            EstimateHelper.StoryPoints = new Dictionary<string, int>();

            Clients.All.resetStory();
        }

        public void DisplayResults()
        {
            Clients.All.displayResults();
        }

        public void SetUser(string user)
        {
            EstimateHelper.ConnectedIds[Context.ConnectionId] = user;

            var allUsers = EstimateHelper.ConnectedIds.Select(x => x.Value).ToArray();

            Clients.All.updateUsers(allUsers);
        }

        public void GetUser()
        {
            if (EstimateHelper.ConnectedIds[Context.ConnectionId] != null)
            {
                Clients.Caller.setUser(EstimateHelper.ConnectedIds[Context.ConnectionId]);
            }
            else
            {
                Clients.Caller.promptUser();
            }
        }

        public override Task OnConnected()
        {
            EstimateHelper.ConnectedIds.Add(Context.ConnectionId, null);

            // send the active story to new users
            if (!string.IsNullOrEmpty(EstimateHelper.CurrentStory))
            {
                Clients.Caller.submitStory(EstimateHelper.CurrentStory);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            EstimateHelper.ConnectedIds.Remove(Context.ConnectionId);
            EstimateHelper.StoryPoints.Remove(Context.ConnectionId);
            
            var allUsers = EstimateHelper.ConnectedIds.Select(x => x.Value).ToArray();

            Clients.All.updateUsers(allUsers);

            return base.OnDisconnected(stopCalled);
        }
    }

    public static class EstimateHelper
    {
        public static string CurrentStory;

        public static IDictionary<string, string> ConnectedIds = new Dictionary<string, string>();

        public static IDictionary<string, int> StoryPoints = new Dictionary<string, int>();
    }
}