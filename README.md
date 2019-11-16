# RucksackProblem
Solutions for the 0/1 variation and the unbounded variation.

![Problem](https://i.imgur.com/DoboSkE.jpg)

0/1 variation: Each item can be added only once

Unbounded variation: Each item can be added as many times needed

The rucksack problem is an example of abstraction, where the mass and price component separated are effectively meaningless,
however the ratio of the two(price per unit mass) can be used to solve the problem and any variation of it. Once forming the ratios, 
the solving variations of the problem is trivial, all that has to be done is to relate the ratio to how the variation requries. E.g, for
the 0/1 variation, the items are simply added from best ratio to worst ratio until the max mass is reached.
To find the ratios, the FindRatios() method is used. This will divide all the price by the mass for each object
and store the result in an Item struct(has the ratio and the index it was found). The output list is sorted from
worst item at index 0 and best at n. Then this list can be iterated according to the variation and the problem is solved.
