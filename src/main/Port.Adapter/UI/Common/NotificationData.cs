﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ei8.Cortex.Diary.Port.Adapter.UI.Common
{
    // TODO: transfer to (cortex diary nucleus client) / (cortex diary nucleus common - if populated in nucleus itself, to prevent diary from referencing nucleus)?
    public struct NotificationData
    {
        public NotificationData(string timestamp, string authorId, string author, string typeName, string type, int version, int expectedVersion, string id, string tag, string data, string details)
        {
            this.Timestamp = timestamp;
            this.AuthorId = authorId;
            this.Author = author;
            this.TypeName = typeName;
            this.Type = type;
            this.Version = version;
            this.ExpectedVersion = expectedVersion;
            this.Id = id;
            this.Tag = tag;
            this.Data = data;
            this.Details = details;
        }

        public string Timestamp { get; private set; }

        public string AuthorId { get; private set; }

        public string Author { get; private set; }

        public string TypeName { get; private set; }

        public string Type { get; private set; }

        public int Version { get; private set; }

        public int ExpectedVersion { get; private set; }

        public string Id { get; private set; }

        public string Tag { get; private set; }

        public string Data { get; private set; }

        public string Details { get; private set; }
    }
}
