using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.Copilot;

namespace Octokit
{
    public class CopilotClient : ApiClient, ICopilotClient
    {
        public CopilotClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        [ManualRoute("GET", "/orgs/{org}/copilot/billing/seats")]
        public async Task<CopilotSeatsResponse> GetAllSeats(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, nameof(org));
            
            var results = await ApiConnection.GetAll<CopilotSeatsResponse>(ApiUrls.CopilotSeats(org), ApiOptions.None);
            if (results.Count == 0)
            {
                return new CopilotSeatsResponse { TotalSeats = 0 };
            }

            var totalSeats = results[0].TotalSeats;
            var seats = new List<CopilotSeat>();

            foreach (var copilotSeatsResponse in results)
            {
                seats.AddRange(copilotSeatsResponse.Seats);
            }

            return new CopilotSeatsResponse { TotalSeats = totalSeats, Seats = seats };
        }
    }
}
