﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TebbyPoker.Models;

namespace TebbyPoker.GameEngine.Contracts
{
    public interface IHandEvaluator
    {
        Combination Evaluate(List<Card> hand);
    }
}
