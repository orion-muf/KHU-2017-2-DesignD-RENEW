﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Controller.DAC
{
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Manager = SoccerTradingSystem.Model.Manager;
    using JSON = List<Dictionary<string, object>>;

    class SystemAccountDAC : MariaDBConnector
    {
        public SystemAccountDAC() : base()
        {
        }
        public JSON authenticate(String email, String password)
        {
            // email, password 기반으로 해당하는 user 정보 검색
            query = $"SELECT * from {userTable} where `email` = '{email}' AND `password` = '{password}'";
            queryResult = execute(query);
            return queryResult;
        }
        public void addPlayerData(Player player)
        {
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{player.email}', '{player.password}','Client');";
            query += $"INSERT INTO {clientTable} (`userId`, `type`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{player.email}') , 'Player');  ";
            query += $"INSERT INTO {playerTable} (clientId, firstName, middleName, lastName, birth, position, weight, height, status)"
                        + $" VALUES ( ( SELECT `clientId` FROM {clientTable} WHERE `userId` =  (SELECT `uid` FROM {userTable} WHERE `email` = '{player.email}')) "
                        + $" ,'{player.firstName}','{player.middleName}','{player.lastName}','{player.birth}','{player.position}','{player.weight}','{player.height}','{player.status}') ";
            queryResult = execute(query);
        }
        public void addClubData(Club club)
        {
            //query = "begin; ";
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{club.email}', '{club.password}','Client');  ";
            query += $"INSERT INTO {clientTable} (`userId`, `type`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{club.email}') , 'Club');  ";
            query += $"INSERT INTO {clubTable} (clientId, birth, name, contactNumber)"
                        + $" VALUES ( ( SELECT `clientId` FROM {clientTable} WHERE `userId` =  (SELECT `uid` FROM {userTable} WHERE `email` = '{club.email}')) "
                        + $" ,'{club.birth}','{club.name}','{club.contactNumber}') ";
            queryResult = execute(query);
        }
        public void addManagerData(Manager manager)
        {
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{manager.email}', '{manager.password}','Manager');  ";
            query += $"INSERT INTO {ManagerTable} (`userId`, `name`, `telNumber`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{manager.email}') , '{manager.name}', '{manager.telNumber}');  ";
            queryResult = execute(query);
        }


        public bool updateUserData(User user)
        {
            query = $"UPDATE {userTable} SET `password`  = {user.password}, `authenticated` = {user.authenticated}'  WHERE uid = {user.uid}; "; 
            queryResult = execute(query);
            return true;
        }
        public bool updatePlayerData(Player player)
        {
            query = $"UPDATE {playerTable} SET `firstName`  = {player.firstName}, `middleName`  = {player.middleName},`lastName`  = {player.lastName},`birth`  = {player.birth}, `position`  = {player.position},`weight`  = {player.weight},`height`  = {player.height},`status`  = {player.status}  WHERE playerId = {player.playerId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool updateClubData(Club club)
        {
            query = $"UPDATE {clubTable} SET `name`  = {club.name}, `contactNumber`  = {club.contactNumber},`birth`  = {club.birth}  WHERE clubId = {club.clubId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool updateManagerData(Manager manager)
        {
            query = $"UPDATE {ManagerTable} SET `name`  = {manager.name}, `telNumber`  = {manager.telNumber}   WHERE managerId = {manager.managerId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool deleterClubData(Club club)
        {
            query = $"DELETE FROM {clubTable} WHERE clubId = {club.clubId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool deletePlayerData(Player player)
        {
            query = $"DELETE FROM {playerTable} WHERE playerid = {player.playerId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool deleteManagerData(Manager manager)
        {
            query = $"DELETE FROM {ManagerTable} WHERE managerId = {manager.managerId}; ";
            queryResult = execute(query);
            return true;
        }
    }
}
