
using DataAccessLayer.Repositories;
using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class korisnikOdabirAnketaService
    {
        //get all votes by user id
        public List<korisnik_odabir_anketa> GetVoteByUserId(int userId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.GetVoteByUserId(userId).ToList();
            }
        }

        //get all votes by poll id
        public List<korisnik_odabir_anketa> GetVoteByPollId(int pollId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.GetVoteByPollId(pollId).ToList();
            }
        }

        //get all votes by choice id
        public List<korisnik_odabir_anketa> GetVoteByChoiceId(int choiceId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.GetVoteByChoiceId(choiceId).ToList();
            }
        }

        //check if user has voted
        public bool hasUserVote(int userId, int pollId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.hasUserVote(userId, pollId);
            }
        }

        //add vote
        public bool AddVote(korisnik_odabir_anketa vote)
        {
            bool isSuccesful = false;
            using (var db = new korisnikOdabirAnketaRepository())
            {
                int affectedRows = db.Add(vote);
                isSuccesful = affectedRows > 0;
            }
            return isSuccesful;
        }

        //update vote
        public void UpdateVote(korisnik_odabir_anketa vote)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                db.Update(vote);
            }
        }

        public List<korisnik_odabir_anketa> getUsersVoteFromPoll(int korisnikId, int pollID)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.getUsersVoteFromPoll(korisnikId, pollID).ToList();
            }
        }
        public Dictionary<int, int> CountVotesForEachChoice(int pollId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.CountVotesForEachChoice(pollId);
            }
        }

        public Dictionary<string, int> CalculatePercentageOfVotesForEachChoice(int pollId)
        {
            using (var db = new korisnikOdabirAnketaRepository())
            {
                return db.CalculatePercentageOfVotesForEachChoice(pollId);
            }
        }
    }
}
