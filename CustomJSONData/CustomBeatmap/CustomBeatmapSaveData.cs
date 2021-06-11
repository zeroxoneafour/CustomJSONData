﻿namespace CustomJSONData.CustomBeatmap
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class CustomBeatmapSaveData : BeatmapSaveData
    {
        public CustomBeatmapSaveData()
            : base(null, null, null, /*longNotes,*/ null, null)
        {
            // Default values for these fields
            // We deserialize using NullValueHandling.Ignore so that these fields do not get overwritten if they are missing in the json string
            _version = string.Empty;
            _customData = Trees.Tree();
            _events = new List<EventData>();
            _notes = new List<NoteData>();
            ////_longNotes = new List<LongNoteData>();
            _waypoints = new List<WaypointData>();
            _obstacles = new List<ObstacleData>();
        }

        [JsonIgnore]
        public List<CustomEventData> customEvents { get; set; } = new List<CustomEventData>();

        [JsonIgnore]
        public dynamic customData { get; protected set; }

        [Serializable]
        private class CustomEventsSaveData
        {
#pragma warning disable 0649

            [JsonProperty]
            public CustomData _customData;

            [Serializable]
            public class CustomData
            {
                [JsonProperty]
                public List<CustomEventData> _customEvents;
            }

#pragma warning restore 0649
        }

        public new static CustomBeatmapSaveData DeserializeFromJSONString(string stringData)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>() { new ExpandoObjectConverter() }
            };
            CustomBeatmapSaveData beatmap = JsonConvert.DeserializeObject<CustomBeatmapSaveData>(stringData, settings);

            CustomEventsSaveData customEvents = JsonConvert.DeserializeObject<CustomEventsSaveData>(stringData);
            if (customEvents._customData != null && customEvents._customData._customEvents != null)
            {
                beatmap.customEvents = customEvents._customData._customEvents;
            }

            return beatmap;
        }

        [JsonProperty]
        internal new string _version
        {
            get => base._version;
            set => base._version = value;
        }

        [JsonProperty]
        internal new List<EventData> _events
        {
            get => base._events.Cast<EventData>().ToList();
            set => base._events = value.Cast<BeatmapSaveData.EventData>().ToList();
        }

        [JsonProperty]
        [JsonConverter(typeof(ExpandoObjectConverter))]
        internal dynamic _customData
        {
            get => customData;
            set => customData = value;
        }

        [JsonProperty]
        internal new List<NoteData> _notes
        {
            get => base._notes.Cast<NoteData>().ToList();
            set => base._notes = value.Cast<BeatmapSaveData.NoteData>().ToList();
        }

        /*[JsonProperty]
        protected new List<LongNoteData> _longNotes
        {
            get => base._longNotes.Cast<LongNoteData>().ToList();
            set => base._longNotes = value.Cast<BeatmapSaveData.LongNoteData>().ToList();
        }*/

        [JsonProperty]
        internal new List<WaypointData> _waypoints
        {
            get => base._waypoints.Cast<WaypointData>().ToList();
            set => base._waypoints = value.Cast<BeatmapSaveData.WaypointData>().ToList();
        }

        [JsonProperty]
        internal new List<ObstacleData> _obstacles
        {
            get => base._obstacles.Cast<ObstacleData>().ToList();
            set => base._obstacles = value.Cast<BeatmapSaveData.ObstacleData>().ToList();
        }

        [JsonProperty]
        internal new SpecialEventKeywordFiltersData _specialEventsKeywordFilters
        {
            get => (SpecialEventKeywordFiltersData)base._specialEventsKeywordFilters;
            set => base._specialEventsKeywordFilters = value;
        }

        [Serializable]
        public new class EventData : BeatmapSaveData.EventData
        {
            public EventData() : base(0, 0, 0)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            internal new float _time
            {
                get => base._time;
                set => base._time = value;
            }

            [JsonProperty]
            internal new BeatmapEventType _type
            {
                get => base._type;
                set => base._type = value;
            }

            [JsonProperty]
            internal new int _value
            {
                get => base._value;
                set => base._value = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            internal dynamic _customData;
        }

        [Serializable]
        public class CustomEventData : ITime
        {
            public CustomEventData()
            {
            }

            [JsonIgnore]
            public float time => _time;

            [JsonIgnore]
            public string type => _type;

            [JsonIgnore]
            public dynamic data => _data;

            public void MoveTime(float offset)
            {
                _time += offset;
            }

            [JsonProperty]
            internal float _time;

            [JsonProperty]
            internal string _type;

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            internal dynamic _data;
        }

        [Serializable]
        public new class NoteData : BeatmapSaveData.NoteData
        {
            public NoteData() : base(0, 0, 0, 0, 0)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            internal new float _time
            {
                get => base._time;
                set => base._time = value;
            }

            [JsonProperty]
            internal new int _lineIndex
            {
                get => base._lineIndex;
                set => base._lineIndex = value;
            }

            [JsonProperty]
            internal new NoteLineLayer _lineLayer
            {
                get => base._lineLayer;
                set => base._lineLayer = value;
            }

            [JsonProperty]
            internal new NoteType _type
            {
                get => base._type;
                set => base._type = value;
            }

            [JsonProperty]
            internal new NoteCutDirection _cutDirection
            {
                get => base._cutDirection;
                set => base._cutDirection = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            internal dynamic _customData;
        }

        [Serializable]
        public new class WaypointData : BeatmapSaveData.WaypointData
        {
            public WaypointData(float time, int lineIndex, NoteLineLayer lineLayer, OffsetDirection offsetDirection) : base(time, lineIndex, lineLayer, offsetDirection)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            internal new float _time
            {
                get => base._time;
                set => base._time = value;
            }

            [JsonProperty]
            internal new int _lineIndex
            {
                get => base._lineIndex;
                set => base._lineIndex = value;
            }

            [JsonProperty]
            internal new NoteLineLayer _lineLayer
            {
                get => base._lineLayer;
                set => base._lineLayer = value;
            }

            [JsonProperty]
            internal new OffsetDirection _offsetDirection
            {
                get => base._offsetDirection;
                set => base._offsetDirection = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            internal dynamic _customData;
        }

        /*[Serializable]
        public new class LongNoteData : BeatmapSaveData.LongNoteData
        {
            public LongNoteData(float time, int lineIndex, NoteLineLayer lineLayer, LongNoteType type, NoteCutDirection cutDirection, float duration)
                : base(time, lineIndex, lineLayer, type, cutDirection, duration)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            protected new float _time
            {
                get => base._time;
                set => base._time = value;
            }

            [JsonProperty]
            protected new int _lineIndex
            {
                get => base._lineIndex;
                set => base._lineIndex = value;
            }

            [JsonProperty]
            protected new NoteLineLayer _lineLayer
            {
                get => base._lineLayer;
                set => base._lineLayer = value;
            }

            [JsonProperty]
            protected new LongNoteType _type
            {
                get => base._type;
                set => base._type = value;
            }

            [JsonProperty]
            protected new NoteCutDirection _cutDirection
            {
                get => base._cutDirection;
                set => base._cutDirection = value;
            }

            [JsonProperty]
            protected new float _duration
            {
                get => base._duration;
                set => base._duration = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            protected dynamic _customData;
        }*/

        [Serializable]
        public new class ObstacleData : BeatmapSaveData.ObstacleData
        {
            public ObstacleData() : base(0, 0, 0, 0, 0)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            internal new float _time
            {
                get => base._time;
                set => base._time = value;
            }

            [JsonProperty]
            internal new int _lineIndex
            {
                get => base._lineIndex;
                set => base._lineIndex = value;
            }

            [JsonProperty]
            internal new ObstacleType _type
            {
                get => base._type;
                set => base._type = value;
            }

            [JsonProperty]
            internal new float _duration
            {
                get => base._duration;
                set => base._duration = value;
            }

            [JsonProperty]
            internal new int _width
            {
                get => base._width;
                set => base._width = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            internal dynamic _customData;
        }

        // _customData for these are currently thrown away, there is not currently a way to retrieve them
        [Serializable]
        public new class SpecialEventKeywordFiltersData : BeatmapSaveData.SpecialEventKeywordFiltersData
        {
            public SpecialEventKeywordFiltersData(List<BeatmapSaveData.SpecialEventsForKeyword> keywords) : base(keywords)
            {
            }

            [JsonProperty]
            protected new List<SpecialEventsForKeyword> _keywords
            {
                get => base._keywords.Cast<SpecialEventsForKeyword>().ToList();
                set => base._keywords = value.Cast<BeatmapSaveData.SpecialEventsForKeyword>().ToList();
            }
        }

        [Serializable]
        public new class SpecialEventsForKeyword : BeatmapSaveData.SpecialEventsForKeyword
        {
            public SpecialEventsForKeyword(string keyword, List<BeatmapEventType> specialEvents) : base(keyword, specialEvents)
            {
            }

            [JsonIgnore]
            public dynamic customData => _customData;

            [JsonProperty]
            protected new string _keyword
            {
                get => base._keyword;
                set => base._keyword = value;
            }

            [JsonProperty]
            protected new List<BeatmapEventType> _specialEvents
            {
                get => base._specialEvents;
                set => base._specialEvents = value;
            }

            [JsonProperty]
            [JsonConverter(typeof(ExpandoObjectConverter))]
            protected dynamic _customData;
        }
    }
}
