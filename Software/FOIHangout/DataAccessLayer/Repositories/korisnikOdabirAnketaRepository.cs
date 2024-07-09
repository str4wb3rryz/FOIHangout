using EntitiesLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class korisnikOdabirAnketaRepository : Repository<korisnik_odabir_anketa>
    {
        public korisnikOdabirAnketaRepository() : base(new FOIHangoutModel())
        {
        }


        public IQueryable<korisnik_odabir_anketa> GetVoteByUserId(int userId)
        {
            var query = from p in Entities where p.id_korisnik == userId select p;
            return query;
        }

        public IQueryable<korisnik_odabir_anketa> GetVoteByPollId(int pollId)
        {
            var query = from p in Entities where p.id_anketa == pollId select p;
            return query;
        }

        public IQueryable<korisnik_odabir_anketa> GetVoteByChoiceId(int choiceId)
        {
            var query = from p in Entities where p.id_odabira == choiceId select p;
            return query;
        }

        public IQueryable<korisnik_odabir_anketa> getUsersVoteFromPoll(int korisnikId, int pollID)
        {
            var query = from p in Entities where p.id_korisnik == korisnikId && p.id_anketa == pollID select p;
            return query;
        }

        public bool hasUserVote(int userId, int pollId)
        {
            var query = from p in Entities where p.id_korisnik == userId && p.id_anketa == pollId select p;
            if (query.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<int, int> CountVotesForEachChoice(int pollId)
        {
            var choiceCounts = Entities
                .Where(p => p.id_anketa == pollId)
                .GroupBy(p => p.id_odabira)
                .ToDictionary(g => g.Key, g => g.Count());

            return choiceCounts;
        }

        public Dictionary<string, int> CalculatePercentageOfVotesForEachChoice(int pollId)
        {
            var choices = Context.anketa
                .Where(p => p.id == pollId)
                .SelectMany(p => p.odabir_selekcije)
                .ToList();
            Dictionary<int, int> voteCounts = CountVotesForEachChoice(pollId);
            int totalVotes = voteCounts.Values.Sum();

            return choices.ToDictionary(
                choice => choice.odabir,
                choice => (int)Math.Round(totalVotes > 0 ? (double)(voteCounts.ContainsKey(choice.id) ? voteCounts[choice.id] : 0) / totalVotes * 100 : 0, MidpointRounding.AwayFromZero)
            );
        }

        public override int Add(korisnik_odabir_anketa entity, bool saveChanges = true)
        {
            var newVote = new korisnik_odabir_anketa
            {
                id_korisnik = entity.id_korisnik,
                id_anketa = entity.id_anketa,
                id_odabira = entity.id_odabira
            };

            Entities.Add(newVote);
            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
        public override int Update(korisnik_odabir_anketa entity, bool saveChanges = true)
        {
            var vote = Entities.Where(v => v.id_korisnik == entity.id_korisnik && v.id_anketa == entity.id_anketa).FirstOrDefault();
            vote.id_odabira = entity.id_odabira;

            if (saveChanges)
            {
                return SaveChanges();
            }
            else
            {
                return 0;
            }
        }
    }
}
