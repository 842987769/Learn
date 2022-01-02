-- Author: Jigger
-- Date: 2021-12
-- 功能描述:工厂模式实例

--简单工厂类
OperationFactory={}

--运算类
Operation={NumberA=0,NumberB=1}
 
function Operation:new(o)
	o=o or {}
	setmetatable(o,self)
	self.__index=self
	return o
end
 
--+-*/子类
OperationAdd=Operation:new()
OperationSub=Operation:new()
OperationMul=Operation:new()
OperationDiv=Operation:new()

function OperationAdd:GetResult()
	if self.NumberA and self.NumberB then
		return self.NumberA+self.NumberB;
	else		
		return "error"
	end
end

function OperationSub:GetResult()
	if self.NumberA and self.NumberB then
		return self.NumberA-self.NumberB;
	else
		return "error"
	end
end

function OperationMul:GetResult()
	if self.NumberA and self.NumberB then
		return self.NumberA*self.NumberB;
	else
		return "error"
	end
end

function OperationDiv:GetResult()
	if self.NumberA and self.NumberB and self.NumberB~=0 then
		return self.NumberA/self.NumberB;
	else
		return "error"
	end
end
 
function OperationFactory : CreateOperation(o)
  local oper={
    ["+"]=OperationAdd:new(),
    ["-"]=OperationSub:new(),
    ["*"]=OperationMul:new(),
    ["/"]=OperationDiv:new()
  }
  return oper[o]
end
 
Oper1=OperationFactory:CreateOperation("+")
Oper1.NumberA=1
Oper1.NumberB=2
print(Oper1:GetResult())
return OperationFactory