using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;

namespace Jh.Abp.MenuManagement
{
    public class AuditLoggingRetrieveInputDto: PagedAndSortedResultRequestDto
    {
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string httpMethod { get; set; }
        public string url { get; set; }
        public string userName { get; set; }
        public string applicationName { get; set; }
        public string correlationId { get; set; }
        public int? maxExecutionDuration { get; set; }
        public int? minExecutionDuration { get; set; }
        public bool? hasException { get; set; }
        public HttpStatusCode? httpStatusCode { get; set; }
    }
}
