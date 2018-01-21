﻿using DiscordRPC.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordRPC
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn, ItemNullValueHandling = NullValueHandling.Ignore)]
	public class RichPresence
	{
		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
		public string State { get; set; }
		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
		public string Details { get; set; }
		
		[JsonProperty("timestamps", NullValueHandling = NullValueHandling.Ignore)]
		public Timestamps Timestamps { get; set; }

		[JsonProperty("assets", NullValueHandling = NullValueHandling.Ignore)]
		public Assets Assets { get; set; }
		
		[JsonProperty("party", NullValueHandling = NullValueHandling.Ignore)]
		public Party Party { get; set; }

		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("match", NullValueHandling = NullValueHandling.Ignore)]
		public string MatchSecret { get; set; }

		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("join", NullValueHandling = NullValueHandling.Ignore)]
		public string JoinSecret { get; set; }

		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("spectate", NullValueHandling = NullValueHandling.Ignore)]
		public string SpectateSecret { get; set; }

		[JsonProperty("instance", NullValueHandling = NullValueHandling.Ignore)]
		public bool Instance { get; set; }

		
	}

	public class Assets
	{
		/// <summary>
		/// Max 32 Bytes.
		/// </summary>
		[JsonProperty("large_image", NullValueHandling = NullValueHandling.Ignore)]
		public string LargeImageKey { get; set; }

		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("large_text", NullValueHandling = NullValueHandling.Ignore)]
		public string LargeImageText { get; set; }

		/// <summary>
		/// Max 32 Bytes.
		/// </summary>
		[JsonProperty("small_image", NullValueHandling = NullValueHandling.Ignore)]
		public string SmallImageKey { get; set; }

		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("small_text", NullValueHandling = NullValueHandling.Ignore)]
		public string SmallImageText { get; set; }
	}

	public class Timestamps
	{
		[JsonIgnore]
		public DateTime? Start { get; set; }

		[JsonIgnore]
		public DateTime? End { get; set; }

		[JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
		private long? epochStart { get { return Start.HasValue ? GetEpoch(Start.Value) : (long?)null; } }

		[JsonProperty("end", NullValueHandling = NullValueHandling.Ignore)]
		private long? epochEnd { get { return End.HasValue ? GetEpoch(End.Value) : (long?)null; } }

		public static long GetEpoch(DateTime time)
		{
			DateTime epochStart = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
			return (long)(time - epochStart).TotalSeconds;
		}
	}

	public class Party
	{
		/// <summary>
		/// Max 128 Bytes.
		/// </summary>
		[JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
		public string ID { get; set; }

		[JsonIgnore]
		public int? Size { get; set; }

		[JsonIgnore]
		public int? Max { get; set; }

		[JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
		private int[] _size
		{
			get
			{
				//We have no size, so its null
				if (!Size.HasValue) return null;

				//It does not have a max size, so just return the size
				if (!Max.HasValue)
					return new int[] { Size.Value, Size.Value };

				//It has max size, return both
				return new int[] { Size.Value, Max.Value };
			}
		}
	}
}