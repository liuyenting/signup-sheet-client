using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace signup_sheet_client.Network
{
    public class Payload
    {
        private State state = null;
        public Payload(string json)
        {
            this.state = JsonConvert.DeserializeObject<State>(json);
        }

        public bool Valid
        {
            get
            {
                if(this.state != null)
                {
                    return this.state.Valid;
                }
                else
                {
                    Console.WriteLine("this.state == null -> Valid = false");
                    return false;
                }
            }
        }

        public bool Due
        {
            get
            {
                if(this.state != null)
                {
                    return this.state.Due;
                }
                else
                {
                    Console.WriteLine("this.state == null -> Due = false");
                    return false;
                }
            }
        }

        public UserInfo User
        {
            get
            {
                if(this.state != null)
                {
                    return this.state.User;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class State
    {
        [JsonProperty("valid")]
        private bool valid = true;
        [JsonIgnore]
        public bool Valid
        {
            get
            {
                return this.valid;
            }
        }

        [JsonProperty("due")]
        private bool due = false;
        [JsonIgnore]
        public bool Due
        {
            get
            {
                return this.due;
            }
        }

        [JsonProperty("user")]
        private UserInfo user;
        [JsonIgnore]
        public UserInfo User
        {
            get
            {
                return this.user;
            }
        }
    }

    public class UserInfo
    {
        [JsonProperty("cardId")]
        private ulong cardId = 0;
        [JsonIgnore]
        public ulong CardId
        {
            get
            {
                return this.cardId;
            }
            // Temporary enable set...
            set
            {
                this.cardId = value;
            }
        }

        [JsonProperty("regId")]
        private string regId = string.Empty;
        [JsonIgnore]
        public string RegId
        {
            get
            {
                return this.regId;
            }
        }

        [JsonProperty("firstName")]
        private string firstName = string.Empty;
        [JsonIgnore]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
        }

        [JsonProperty("lastName")]
        private string lastName = string.Empty;
        [JsonIgnore]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
        }
    }
}
