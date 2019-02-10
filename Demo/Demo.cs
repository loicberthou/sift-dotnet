﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Sift;

namespace Demo
{
    class Demo
    {
        static void Main(string[] args)
        {
            var sift = new Sift.Client("");

            // Construct reserved events with known fields
            var createOrder = new CreateOrder
            {
                user_id = "gary",
                order_id = "oid",
                amount = 1000000,
                currency_code = "USD",
                billing_address = new Address {
                    name = "gary",
                    city = "san francisco"
                },
                app = new App {
                    app_name = "com.example.myapp.purchases",
                    app_version = "1.0"
                },
                items = new ObservableCollection<Item>() { new Item{sku="abc"}, new Item{sku="abc"} }
            };

            // Augment with custom fields
            createOrder.AddField("foo", "bar");

            try 
            {
                EventResponse res = sift.Send(new EventRequest
                {
                    Event = createOrder,
                    ReturnScore = true,
                    AbuseTypes = new List<string>() { "payment_abuse", "account_takeover" }
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Construct custom events with required fields
            var makeCall = new CustomEvent
            {
                type = "make_call",
                user_id = "gary"
            };

            // Augment with custom fields
            makeCall.AddFields(new Dictionary<string, object>
            {
                ["foo"] = "bar",
                ["payment_status"] = "$success"
            });

            try
            {
                EventResponse res = sift.Send(new EventRequest
                {
                    Event = makeCall
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Get score
            try
            {
                ScoreResponse res = sift.Send(new ScoreRequest
                {
                    UserId = "gary"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Rescore
            try
            {
                ScoreResponse res = sift.Send(new RescoreRequest
                {
                    UserId = "gary"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Label
            try
            {
                SiftResponse response = sift.Send(new LabelRequest
                {
                    UserId = "gary",
                    IsBad = true,
                    AbuseType = "payment_abuse"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Unlabel
            try
            {
                SiftResponse response = sift.Send(new UnlabelRequest
                {
                    UserId = "gary",
                    AbuseType = "payment_abuse"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Apply Decision
            try
            {
                ApplyDecisionResponse response = sift.Send(new ApplyUserDecisionRequest
                {
                    AccountId = "your-account-id",
                    UserId = "gary",
                    DecisionId = "zoodles_payment_abuse_3",
                    Source = "AUTOMATED_RULE"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Get Decision Status
            try
            {
                GetDecisionStatusResponse response = sift.Send(new GetDecisionStatusRequest
                {
                    AccountId = "your-account-id",
                    UserId = "gary"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Get Decisions
            try
            {
                GetDecisionsResponse response = sift.Send(new GetDecisionsRequest
                {
                    AccountId = "your-account-id"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }



            // Workflow Status
            try
            {
                WorkflowStatusResponse response = sift.Send(new WorkflowStatusRequest
                {
                    AccountId = "your-account-id",
                    WorkflowRunId = "syj22bfrn2hqi"
                }).Result;
            }
            catch (AggregateException ae)
            {
                // Handle InnerException
            }
        }
    }
}