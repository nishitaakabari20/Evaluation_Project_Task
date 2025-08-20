using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Recipe.Model.CommonModel
{
    public class SearchPage<T>
    {
        /// <summary>
        /// Constructor initializes default values.
        /// </summary>
        public SearchPage()
        {
            List = new List<T>();
            Meta = new Meta();
        }

        /// <summary>
        /// Meta information for the search.
        /// </summary>
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// List of results.
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<T> List { get; set; }
    }

    /// <summary>
    /// Metadata information for pagination and additional info.
    /// </summary>
    public class Meta
    {
        public Meta()
        {
            ExtraData = new JArray();
        }

        /// <summary>
        /// The current page number. The first page is 1.
        /// </summary>
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }

        /// <summary>
        /// Resource key name.
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        /// <summary>
        /// The URL of the current page.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        /// <summary>
        /// The URL for the first page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "first_page_url")]
        public string FirstPageUrl { get; set; }

        /// <summary>
        /// The URL for the previous page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "previous_page_url")]
        public string PreviousPageUrl { get; set; }

        /// <summary>
        /// The URL for the next page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "next_page_url")]
        public string NextPageUrl { get; set; }

        /// <summary>
        /// Total count of results.
        /// </summary>
        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Total number of pages in the result set.
        /// </summary>
        [JsonProperty(PropertyName = "total_page_num")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Additional metadata or dynamic information.
        /// </summary>
        [JsonProperty(PropertyName = "extra_data")]
        public object ExtraData { get; set; }

        /// <summary>
        /// Indicates whether there is a next page.
        /// </summary>
        [JsonIgnore] // Now only Newtonsoft.Json.JsonIgnore is in use
        [JsonProperty(PropertyName = "next_page_exists")]
        public bool NextPageExists { get; set; }
    }
}
