using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using OMNext.Models;
using OMNext.Data;
using Microsoft.AspNetCore.Hosting;

namespace OMNext.Hubs
{
    public class ChatHub : Hub
    {
        private readonly OM2018Context _context;
        private IWebHostEnvironment _env;

        public ChatHub(OM2018Context context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // group, UserAgent and missionID are synonymous. I made a group based on the running mission
        // so that all chat and data stay within that mission's scope. UserAgent was a field in a table
        // from an example I had found online to store connections. I use it to store the missionID.

        /// <summary>
        /// Add the user to the group
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <returns></returns>
        public async Task AddToGroup(int group)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, group.ToString());
                await base.OnConnectedAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Sends chat messages to the group.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="group">the mission id</param>
        /// <param name="sendFrom">the interface sending the message</param>
        /// <param name="sendTo">the intended recipients of the message</param>
        /// <param name="message">the message</param>
        /// <returns></returns>
        public async Task SendMessageToGroup(string user, int group, string sendFrom, string sendTo, string message)
        {
            // make call to javascript function
            await Clients.Group(group.ToString()).SendAsync("ReceiveMessage", user, sendFrom, sendTo, message);
        }

        /// <summary>
        /// Displays the current data from the flight director data push on the team interfaces.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="team">the team</param>
        /// <param name="currentRecord">the current record to be displayed</param>
        /// <returns></returns>
        public async Task SendDataPushToGroup(int group, string team, int currentRecord, int currentTimerInterval)
        {
            try
            {
                // make call to javascript function
                await Clients.Group(group.ToString()).SendAsync("ReceiveDataPush", team, currentRecord, currentTimerInterval);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Displays the input values from the teams on the delegated interfaces.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="data">the passed in data</param>
        /// <returns></returns>
        public async Task SendDataDriveData(string group, List<DataDriveData> data)
        {
            try
            {
                // make call to javascript function
                await Clients.Group(group).SendAsync("ReceiveDataDriveData", data);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Displays the logged in status of the teams on the flight director interface
        /// </summary>
        /// <param name="group"></param>
        /// <param name="user"></param>
        /// <param name="connected"></param>
        /// <returns></returns>
        public async Task ReceiveConnectionState(int group, string user, bool connected)
        {
            // make call to javascript function
            if (user != null)
                await Clients.Group(group.ToString()).SendAsync("ReceiveConnectionState", user, connected);
        }

        /// <summary>
        /// Get the data pushed from flight director interface to display on the team interfaces.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="team">the team</param>
        /// <returns></returns>
        public async Task GetFDDataPushRecord(int group, string team)
        {
            // fires on connect/reconnect
            try
            {
                int teamID = await getDBTeamID(group, team);

                DataDrive dd = await _context.DataDrives
                    .OrderByDescending(o => o.MissionID)
                    .FirstOrDefaultAsync(x => x.MissionID == group && x.TeamID == teamID);

                if (dd != null)
                {
                    // make call to javascript function
                    await Clients.Group(group.ToString()).SendAsync("ReceiveDataPush", team, dd.CurrentRecord, dd.CurrentTimerInterval);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        private async Task<int> getDBTeamID(int missionID, string team)
        {
            // select the correct team for the data push
            int teamNameID = 5;

            if (team == "Hurricane")
                teamNameID = 1;
            else if (team == "Volcano")
                teamNameID = 2;
            else if (team == "MedComm")
                teamNameID = 4;

            // get the teamname from the enum
            TeamName tn = (TeamName)teamNameID;

            // get the team details
            Team t = await _context.Teams.FirstOrDefaultAsync(x => x.MissionID == missionID && x.TeamName == (TeamName)teamNameID);

            // extract the team id
            int teamID = t.TeamID;

            return teamID;
        }

        /// <summary>
        /// Get the data sent by the teams for display purposes.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <returns></returns>
        public async Task GetDataDriveData(int group)
        {
            try
            {
                List<DataDriveData> ddd = await _context.DataDriveDatas
                    .OrderByDescending(o => o.MissionID)
                    .Where(x => x.MissionID == group).ToListAsync();
                if (ddd.Count > 0)
                    await SendDataDriveData(group.ToString(), ddd);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Gets the current time for display on the flight director and communication team's interface.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="gmt">the current time</param>
        /// <returns></returns>
        public async Task GetCurrentGMT(int group, string gmt)
        {
            // make call to javascript function
            await Clients.Group(group.ToString()).SendAsync("ReceiveCurrentGMT", gmt);
        }

        /// <summary>
        /// Gets the current advisory for display on the flight director and communication team's interface.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="adv">the current advisory</param>
        /// <returns></returns>
        public async Task GetCurrentADV(int group, string adv)
        {
            // make call to javascript function
            await Clients.Group(group.ToString()).SendAsync("ReceiveCurrentADV", adv);
            //await Clients.Group(group.ToString()).SendAsync()
        }

        /// <summary>
        /// Gets the current time for display on the flight director and communication team's interface.
        /// </summary>
        /// <param name="group">the mission id</param>
        /// <param name="gmt">the current time</param>
        /// <returns></returns>
        public async Task GetCurrentMCGMT(int group, string gmt)
        {
            // make call to javascript function
            await Clients.Group(group.ToString()).SendAsync("ReceiveCurrentMCGMT", gmt);
        }

        /// <summary>
        /// Gets the list of currently logged in users and updates the flight director interface to show teams that are logged in.
        /// This will sometimes show false logs depending on how the user got disconnected.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public async Task GetLoggedInUsers(int group)
        {
            try
            {
                List<Connection> connectedUsers = await _context.Connections.Where(x => x.UserAgent == group.ToString() && x.Connected == true && x.UserName != null).ToListAsync();

                foreach (Connection item in connectedUsers)
                {
                    await ReceiveConnectionState(group, item.UserName, item.Connected);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Add the user to the group
        /// </summary>
        /// <param name="username">the team logging in, will be null for flight director</param>
        /// <param name="group">the mission id</param>
        /// <returns></returns>
        public async Task Connect(string username, int group)
        {
            try
            {
                if ((username != null || username != "") && group > 0)
                {
                    Connection c = await _context.Connections
                        .OrderBy(o => o.UserAgent).ThenBy(tb => tb.UserName)
                        .FirstOrDefaultAsync(x => x.UserAgent == group.ToString() && x.UserName == username && x.Connected == true);
                    if (c != null)
                    {
                        c.Connected = false;
                        _context.Attach(c);
                        _context.Entry(c).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }

                Connection conn = new Connection()
                {
                    ConnectionID = Context.ConnectionId,
                    UserAgent = group.ToString(),
                    Connected = true,
                    UserName = username
                };
                _context.Connections.Add(conn);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        /// <summary>
        /// Mark the current user as offline
        /// </summary>
        /// <param name="exception">Not being used, but need to have it because of the base class definition</param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connID = Context.ConnectionId;

            string missionID = _context.Connections
                .Where(c => c.ConnectionID == connID)
                .Select(s => s.UserAgent)
                .SingleOrDefault();

            try
            {
                Connection conn = await _context.Connections.FirstOrDefaultAsync(x => x.ConnectionID == connID);
                conn.Connected = false;
                _context.Attach(conn);
                _context.Entry(conn).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                await ReceiveConnectionState(conn.UserAgent, conn.UserName, conn.Connected);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + ": " + e.StackTrace);
            }
        }

        private Task ReceiveConnectionState(string userAgent, string userName, bool connected)
        {
            return ReceiveConnectionState(Int32.Parse(userAgent), userName, connected);
        }
    }
}