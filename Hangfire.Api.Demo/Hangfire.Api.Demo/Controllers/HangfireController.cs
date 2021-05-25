using Microsoft.AspNetCore.Mvc;
using System;

namespace Hangfire.Api.Demo.Controllers
{
    public class HangfireController : BasePublicController
    {
        [HttpPost]
        public IActionResult Welcome()
        {
            //Fire and Forget Job
            var jobId = BackgroundJob.Enqueue(() => SendWelcomeEmail("Welcome to our app"));
            return Ok($"Job Id: {jobId}. Welcome email sent to the user!");
        }

        [HttpPost]
        public IActionResult DisCount()
        {
            //Delayed Job
            int timeInSeconds = 30;
            var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("Welcome to our app"), TimeSpan.FromSeconds(timeInSeconds));
            return Ok($"Job Id: {jobId}. DisCount email will be sent in {timeInSeconds} seconds!");
        }

        [HttpPost]
        public IActionResult DatabaseUpdate()
        {
            //Recurring Job
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated 1"), Cron.Minutely);
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated 2"), "30 */18 * * *", TimeZoneInfo.Local);
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("Database updated 3"), "10 05 * * *", TimeZoneInfo.Local);
            return Ok("Database check job initiated!");
        }

        [HttpPost]
        public IActionResult Confirm()
        {
            //Continuous Jobs
            int timeInSeconds = 30;
            var parentJobId = BackgroundJob.Schedule(() => Console.WriteLine("You asked to be unsubscribed!"), TimeSpan.FromSeconds(timeInSeconds));
            BackgroundJob.ContinueJobWith(parentJobId, () => Console.WriteLine("You were unsubscribed"));
            return Ok("Confirmation job created!");
        }

        [HttpGet]
        public void SendWelcomeEmail(string text)
        {
            Console.WriteLine(text);
        }
    }
}
