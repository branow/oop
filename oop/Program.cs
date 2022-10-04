
public class UserAccount {

    public static void Main(string[] args) {
        UserAccount u1 = new UserAccount("player1");
        UserAccount u2 = new UserAccount("player2");
        Game g1 = new Game(u1, u2, 1000);
        Game g2 = new Game(u1, u2, 800);
        Game g3 = new Game(u1, u2, 400);
        Game g4 = new Game(u1, u2, 300);
        try {
            Game g5 = new Game(u1, u2, -100);
        } catch (ArgumentOutOfRangeException e) {
            Console.WriteLine(e);
        }
        g1.play();
        g2.play();
        g3.play();
        g4.play();
        Console.WriteLine();
        u1.GetStats();
        Console.WriteLine();
        u2.GetStats();
    }

    public string UserName { get; }
    private int currentRating;
    public int CurrentRating
    {
        get { return currentRating; }
        set
        {
            currentRating = value;
            if (currentRating < 1) {
                currentRating = 1;
            }
        }
    }
    public int GamesCount { get; set; }
    public List<Game> Games { get; }

    public UserAccount(string UserName) {
        this.UserName = UserName;
        CurrentRating = 1;
        GamesCount = 0;
        Games = new List<Game>();
    }

    public void WinGame(Game game) {
        CurrentRating = CurrentRating + game.Rating;
        Games.Add(game);
        GamesCount++;
    }

    public void LostGame(Game game) {
        CurrentRating = CurrentRating - game.Rating;
        Games.Add(game);
        GamesCount++;
    }

    public void GetStats() {
        Console.WriteLine(UserName + " played " + GamesCount + " games, " + CurrentRating + " rating");
        foreach (Game g in Games) {
            bool win = g.Winner.Equals(UserName) ? true : false;
            if (win) {
                Console.WriteLine("Won: game -> " + g.Id + ", rating -> " + g.Rating + ", opponent -> " + g.Loser);
            } else {
                Console.WriteLine("Lost: game -> " + g.Id + ", rating -> " + g.Rating + ", opponent -> " + g.Winner);
            }
        }
    }

}


public class Game {

    private static int count;
    public int Id { get; }
    public UserAccount[] Gamers { get; }
    public int Rating { get; }
    public string Winner { get; set; }
    public string Loser { get; set; }

    public Game(UserAccount player1, UserAccount player2, int rating) {
        this.Gamers = new UserAccount[2] {player1, player2};
        int[] i = { 4, 5 };
        if (rating < 0) throw new ArgumentOutOfRangeException(nameof(rating), "Must be > 0");
        this.Rating = rating;
        Id = count++;
    }

    public void play() { 
        Random random = new Random();
        int value = random.Next(Gamers.Length);
        if (0 == value) {
            Gamers[0].WinGame(this);
            Gamers[1].LostGame(this);
            Winner = Gamers[0].UserName;
            Loser = Gamers[1].UserName;
        } else {
            Gamers[1].WinGame(this);
            Gamers[0].LostGame(this);
            Winner = Gamers[1].UserName;
            Loser = Gamers[0].UserName;
        }
    }





}
