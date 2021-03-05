using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using api.Models;

namespace api.Controllers
{
  [ApiController]
  [Route("channel")]
  public class ChannelsController : ControllerBase
  {
    private Channel[] _channels =
    {
        new Channel(
    Name: "Rede Vida",
    Primary:
        "https://www.youtube.com/channel/UC7MUmXqD_kEChxYFME0bdtg/live",
    Secondary:
        "https://cvd1.cds.ebtcvd.net/live-redevida/smil:redevida.smil/playlist.m3u8",
    Logo:
        "https://yt3.ggpht.com/ytc/AAUvwngEnqdSn-GwgmDpm8bRqDac4ifdEKEzPQWWmVu6Mw=s176-c-k-c0x00ffffff-no-rj-mo"),
       new Channel(
        Name: "Rede Século 21",
        Primary:
            "https://www.youtube.com/channel/UC0APLxALWhTrAA00T4Y7Ulw/live",
        Secondary: "https://dhxt2zok2aqcr.cloudfront.net/live/rs21.m3u8",
        Logo:
            "https://yt3.ggpht.com/ytc/AAUvwniiad2DaiQ3OxyyO8m6Di-eXXoT-UsIFq8wZxfzNpA=s176-c-k-c0x00ffffff-no-rj-mo"),
    new Channel(
        Name: "Canção Nova",
        Primary:
            "https://www.youtube.com/channel/UCVrKQMmA2ew9LFzeIDaOFgw/live",
        Secondary:
            "http://tvajuhls-lh.akamaihd.net:80/i/tvdesk_1@147040/master.m3u8",
        Logo:
            "https://yt3.ggpht.com/ytc/AAUvwngMRf5HNCB0DfDHOcRqJ_pW_Z67lPtAwh14RdZCJg=s88-c-k-c0x00ffffff-no-rj"),
   new Channel(
        Name: "Tv Evangelizar",
        Primary: null,
        Secondary:
            "https://5f593df7851db.streamlock.net/evangelizar/tv/playlist.m3u8",
        Logo:
            "https://yt3.ggpht.com/ytc/AAUvwniHT0Rgo48OzPyqzqoIhWNkbhdPwcjgUl27zON6zg=s176-c-k-c0x00ffffff-no-rj-mo"),
   new Channel(
        Name: "TV Aparecida",
        Primary:
            "https://www.youtube.com/channel/UCfYrK5JU5EznsnK3wQE7iIg/live",
        Secondary:
            "https://caikron.com.br:8082/padroeira/padroeira/playlist.m3u8",
        Logo:
            "https://yt3.ggpht.com/ytc/AAUvwnjglMhubaA1iCiJhDRp1AFbM2vbH7eo13Q_S1qpYg=s176-c-k-c0x00ffffff-no-rj-mo"),
   new Channel(
        Name: "Pai Eterno",
        Primary: null,
        Secondary:
            "https://59f1cbe63db89.streamlock.net:1443/teste01/_definst_/teste01/playlist.m3u8",
        Logo:
            "https://yt3.ggpht.com/ytc/AAUvwnhun4onLfGYyvE5RgJUrlCP-IOtCx7iq8w8fNNPnQ=s176-c-k-c0x00ffffff-no-rj-mo")
    };


    private HttpClient _http = new HttpClient();

    [HttpGet("{index}")]
    public async Task<String> DataSource(int index)
    {

      Channel channel = _channels[index];
      try
      {

        if (channel.Primary != null)
        {

          String dataSource = "";
          OptionSet options = new OptionSet()
          {
            Format = "[height=720]",
            GetUrl = true,
          };

          YoutubeDLProcess youtubeProcess = new YoutubeDLProcess("youtube-dl");

          youtubeProcess.OutputReceived += (o, e) => dataSource = e.Data;
          youtubeProcess.ErrorReceived += (o, e) => dataSource = channel.Secondary;

          string[] urls = new[] { channel.Primary };
          await youtubeProcess.RunAsync(urls, options);

          return dataSource;
        }
        else
        {
          return channel.Secondary;
        }
      }
      catch (Exception e)
      {
        Console.Write(e);
        return channel.Secondary;
      }
    }

    [HttpGet("logos")]
    public String[] Logos()
    {
      return _channels.Select((channel) => channel.Logo).ToArray();
    }

  }
}