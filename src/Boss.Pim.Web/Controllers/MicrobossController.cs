using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Abp.Domain.Repositories;
using Boss.Pim.ShareProfits;
using Boss.Pim.ShareProfits.Values;

namespace Boss.Pim.Web.Controllers
{
    public class MicrobossController : Controller
    {
        public IRepository<PartnerAgent, Guid> PartnerAgentRepository { get; set; }
        // GET: Microboss
        public ActionResult Index()
        {
            //市级代理
            int cityAgentC = 30 * 10000;
            int cityAgentB = 48 * 10000;
            int cityAgentA = 72 * 10000;

            //普通代理
            int agentC = 3 * 10000;
            int agentB = 6 * 10000;
            int agentA = 12 * 10000;

            //课程学员
            int monthStu = 699;
            int yearStu = 6000;

            return View();
        }

        public ActionResult CityAgent(LevelRule grade)
        {
            List<LevelRule> list = new List<LevelRule>();
            list.Add(new LevelRule
            {
                Name = "市级代理A",
                LevelType = LevelType.市级代理,
                Amount = 72 * 10000,
                Proportion = 0.5m,
                ProductQty = 240
            });
            list.Add(new LevelRule
            {
                Name = "市级代理B",
                LevelType = LevelType.市级代理,
                Amount = 48 * 10000,
                Proportion = 0.56m,
                ProductQty = 143
            });
            list.Add(new LevelRule
            {
                Name = "市级代理C",
                LevelType = LevelType.市级代理,
                Amount = 30 * 10000,
                Proportion = 0.62m,
                ProductQty = 81
            });

            list.Add(new LevelRule
            {
                Name = "代理A",
                LevelType = LevelType.代理,
                Amount = 12 * 10000,
                Proportion = 0.68m,
                ProductQty = 30
            });
            list.Add(new LevelRule
            {
                Name = "代理B",
                LevelType = LevelType.代理,
                Amount = 6 * 10000,
                Proportion = 0.74m,
                ProductQty = 14
            });
            list.Add(new LevelRule
            {
                Name = "代理C",
                LevelType = LevelType.代理,
                Amount = 3 * 10000,
                Proportion = 0.8m,
                ProductQty = 7
            });


            list.Add(new LevelRule
            {
                Name = "年费会员",
                LevelType = LevelType.课程推广大使,
                Amount = 6000,
                Proportion = 60,
                ProductQty = 7
            });
            list.Add(new LevelRule
            {
                Name = "年费会员",
                LevelType = LevelType.课程推广大使,
                Amount = 6000,
                Proportion = 15,
                ProductQty = 7
            });
            list.Add(new LevelRule
            {
                Name = "月费会员",
                LevelType = LevelType.课程学员,
                Amount = 699,
                Proportion = 5,
                ProductQty = 7
            });


            //普通代理
            int agentC = 3 * 10000;

            //课程学员
            int monthStu = 699;
            int yearStu = 6000;

            //市级代理
            int cityAgentC = 30 * 10000;

            decimal proportion = 0.62m;

            //30W，81个年费会员
            //

            return View();
        }

        public ActionResult CityAgentC(LevelRule grade)
        {

            //公司收到了120*10000  3个C
            //服务243个年费会员
            //3703.70 元 1年




            //30*10000

            //普通代理
            int agentC = 3 * 10000;

            //课程学员
            int monthStu = 699;
            int yearStu = 6000;

            //市级代理
            int cityAgentC = 30 * 10000;

            decimal proportion = 0.62m;

            //30W，81个年费会员
            //

            return View();
        }

        public void BuyPartner(LevelRule levelRule, long fromUserId, long toUserId)
        {
            var fromInfo = PartnerAgentRepository.FirstOrDefault(a => a.UserId == fromUserId);
            fromInfo.ProductQty -= levelRule.ProductQty;
            if (fromInfo.ProductQty < 0)
            {
                //数量不足，需要补货，异常
            }
            var toInfo = PartnerAgentRepository.FirstOrDefault(a => a.UserId == toUserId);
            if (toInfo == null)
            {
                PartnerAgentRepository.Insert(new PartnerAgent
                {
                    ProductQty = levelRule.ProductQty,
                    UserId = toUserId,

                });
            }
            else
            {
                toInfo.ProductQty += levelRule.ProductQty;
            }


        }
    }
}