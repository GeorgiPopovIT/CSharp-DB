namespace MusicHub
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            // DbInitializer.ResetDatabase(context);

            //Test your solutions here
            //Console.WriteLine(ExportAlbumsInfo(context, 9));
            Console.WriteLine(ExportSongsAboveDuration(context, 4));

        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var info = context.Albums
                 .Where(x => x.ProducerId == producerId)
                 .Select(x => new
                 {
                     AlbumName = x.Name,
                     AlbumDate = x.ReleaseDate,
                     ProducerName = x.Producer.Name,
                     AlbumSongs = x.Songs.Select(s => new
                     {
                         SongName = s.Name,
                         SongPrice = s.Price,
                         WriterName = s.Writer.Name
                     })
                     .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.WriterName)
                    .ToList(),
                     AlbumPrice = x.Songs.Sum(s => s.Price)
                 })
                 .OrderByDescending(x => x.AlbumPrice).ToList();


            int row = 1;
            foreach (var album in info)
            {
                sb
                    .AppendLine($"-AlbumName: {album.AlbumName}")
                    .AppendLine($"-ReleaseDate: {album.AlbumDate:MM/dd/yyyy}")
                    .AppendLine($"-ProducerName: {album.ProducerName}")
                    .AppendLine("-Songs:");
                foreach (var song in album.AlbumSongs)
                {
                    sb
                        .AppendLine($"---#{row++}")
                        .AppendLine($"---SongName: {song.SongName}")
                        .AppendLine($"---Price: {song.SongPrice:f2}")
                        .AppendLine($"---Writer: {song.WriterName}");
                }
                sb.AppendLine($"-AlbumPrice: {album.AlbumPrice:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            StringBuilder sb = new StringBuilder();

            var songs = context.Songs
                .Include(x => x.Album)
                .ThenInclude(x => x.Producer)
                .Include(x => x.Writer)
                .ToList()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    SongName = s.Name,
                    PerformerName = s.SongPerformers
                    .Select(s => s.Performer.FirstName + " " + s.Performer.LastName)
                    .FirstOrDefault(),
                    WriterName = s.Writer.Name,
                    AlbumProdecure = s.Album.Producer.Name,
                    SongDuration = s.Duration
                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.WriterName)
                .ThenBy(s => s.PerformerName)
                .ToList();

            int row = 1;
            foreach (var song in songs)
            {
                sb
                    .AppendLine($"-Song #{row++}")
                    .AppendLine($"---SongName: {song.SongName}")
                    .AppendLine($"---Writer: {song.WriterName}")
                    .AppendLine($"---Performer: {song.PerformerName}")
                    .AppendLine($"---AlbumProducer: {song.AlbumProdecure}")
                    .AppendLine($"---Duration: {song.SongDuration.ToString("c")}");
            }

            return sb.ToString().Trim();
        }
    }
}
