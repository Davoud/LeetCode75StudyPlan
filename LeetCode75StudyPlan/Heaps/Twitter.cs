using System.Dynamic;

namespace LeetCode75StudyPlan.Heaps;

class Twitter
{
    class Tw
    {
        public Tw() => Followees.Add(this);
        public readonly List<(int id, long time)> Posts = new();
        public readonly HashSet<Tw> Followees = new();
    }

    private readonly Dictionary<int, Tw> db = new();
    private long time = long.MaxValue;    

    public void PostTweet(int userId, int tweetId)
    {
        if (!db.TryGetValue(userId, out Tw? tw))
        {
            tw = new();
            db[userId] = tw;
        }
        tw.Posts.Add((tweetId, --time));
    }

    public IList<int> GetNewsFeed(int userId)
    {
        var list = new List<int>();

        if (!db.TryGetValue(userId, out Tw? tw)) return list;
        
        var pq = new PriorityQueue<int, long>();
        
        foreach (Tw followee in tw.Followees)
        {
            foreach ((int id, long time) in followee.Posts)
            {
                pq.Enqueue(id, time);               
            }
        }

        while (pq.Count > 0 && list.Count < 10)
            list.Add(pq.Dequeue());

        return list;
    }

    public void Follow(int followerId, int followeeId)
    {
        if (!db.TryGetValue(followerId, out Tw? follower))
        {
            follower = new Tw();
            db[followerId] = follower;
        }
        
        if (!db.TryGetValue(followeeId, out Tw? followee))
        {
            followee = new Tw();
            db[followeeId] = followee;
        }

        follower.Followees.Add(followee);
    }

    public void Unfollow(int followerId, int followeeId)
    {
        if (db.TryGetValue(followerId, out Tw? follower) && db.TryGetValue(followeeId, out Tw? followee))
            follower.Followees.Remove(followee);
    }   
}



public class TwitterTests : ITestable
{
    public void RunTests()
    {
        "355. Design Twitter (testcase 1)".WriteLine();

        Twitter twitter = new Twitter();
        twitter.PostTweet(1, 5); // User 1 posts a new tweet (id = 5).
        var feed = twitter.GetNewsFeed(1);  // User 1's news feed should return a list with 1 tweet id -> [5]. return [5]
        WriteResult(feed.SequenceEqual(List(5)), List(5).Str(), feed.Str());
        twitter.Follow(1, 2);    // User 1 follows user 2.
        twitter.PostTweet(2, 6); // User 2 posts a new tweet (id = 6).
        feed = twitter.GetNewsFeed(1);  // User 1's news feed should return a list with 2 tweet ids -> [6, 5]. Tweet id 6 should precede tweet id 5 because it is posted after tweet id 5.
        WriteResult(feed.SequenceEqual(List(6, 5)), List(6, 5).Str(), feed.Str());
        twitter.Unfollow(1, 2);  // User 1 unfollows user 2.
        feed = twitter.GetNewsFeed(1);  // User 1's news feed should return a list with 1 tweet id -> [5], since user 1 is no longer following user 2.
        WriteResult(feed.SequenceEqual(List(5)), List(5).Str(), feed.Str());
    }
}