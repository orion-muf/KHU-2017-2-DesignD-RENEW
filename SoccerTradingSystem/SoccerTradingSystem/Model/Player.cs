﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Player : Client
    {
        public int playerId { get; set; } = -1; // unique player id
        public String firstName { get; set; } = "";
        public String middleName { get; set; } = "";
        public String lastName { get; set; } = "";
        public int birth { get; set; } = -1;
        public String position { get; set; } = "";
        public int recentRate { get; } = -1;
        public int weight { get; set; } = -1;
        public int height { get; set; } = -1;
        public String status { get; } = "";
        public List<Club> clubs { get; }

        public Player(int uid, String email, String password, bool authenticated, int clientId, List<BankAccount> bankAccounts, int playerId, String firstName, String middleName, String lastName,
            int birth, String position, int recentRate, int weight, int height, String status, List<Club> clubs) : base(uid, email, password, authenticated, clientId, bankAccounts)
        {
            this.playerId = playerId;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.birth = birth;
            this.position = position;
            this.recentRate = recentRate;
            this.height = height;
            this.weight = weight;
            this.status = status;
            this.clubs = clubs;
        }
    }
}
