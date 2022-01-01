-- Author: Jigger
-- Date: 2021-12
-- 功能描述:策略模式实例

--客户端类
Strategy = {}

--调用类
Context = {strategy = nil}
 
function Strategy:new(o)
	o = o or {}
	setmetatable(o,self)
	self.__index = self
	return o;
end
 
function Strategy:AlgorithmInterface() end

--策略ABC 
ConcreteStrategyA = Strategy:new()
ConcreteStrategyB = Strategy:new()
ConcreteStrategyC = Strategy:new()
 
function ConcreteStrategyA:AlgorithmInterface()
	print("算法A实现")
end

function ConcreteStrategyB:AlgorithmInterface()
	print("算法B实现")
end

function ConcreteStrategyC:AlgorithmInterface()
	print("算法C实现")
end
 
function Context:new(s)
	o = {}
	setmetatable(o,self)
	self.__index = self
	if s ~= nil then
		o.strategy = s
	end
	return o;
end

function Context:ContextInterface()
	self.strategy:AlgorithmInterface()
end
 
context1=Context:new(ConcreteStrategyA:new())
context2=Context:new(ConcreteStrategyB:new())
context3=Context:new(ConcreteStrategyC:new())
context1:ContextInterface()
context2:ContextInterface()
context3:ContextInterface()