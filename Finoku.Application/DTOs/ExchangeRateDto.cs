using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finoku.Application.DTOs
{
    public record ExchangeRateDto
    (
        string result,
        decimal conversion_rate,
        string base_code,
        string target_code
    );
}

// // This will return the exchange rate from your base code to the target currency you supplied, as well as a conversion of the amount you supplied:
//{
//    "result": "success",
//	"documentation": "https://www.exchangerate-api.com/docs",
//	"terms_of_use": "https://www.exchangerate-api.com/terms",
//	"time_last_update_unix": 1585267200,
//	"time_last_update_utc": "Fri, 27 Mar 2020 00:00:00 +0000",
//	"time_next_update_unix": 1585270800,
//	"time_next_update_utc": "Sat, 28 Mar 2020 01:00:00 +0000",
//	"base_code": "EUR",
//	"target_code": "GBP",
//	"conversion_rate": 0.8412,
//	"conversion_result": 5.8884
//}


// // Error Responses
    //{
    //    "result": "error",
    //	"error-type": "unknown-code"
    //}