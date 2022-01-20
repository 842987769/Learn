-- Author: Jigger
-- Date: 2021-12
-- 功能描述:单链表Linkedlist

Linkedlist = {}

-- 初始化，构建一个空表
function Linkedlist:New()
    self.__index = self
    self.length = 0
    self.next = nil
    print("链表初始化成功")
    return setmetatable({},self)
end

function Linkedlist:CheckList()
    if not self then print("链表不存在") return end
end

function Linkedlist:CheckIndex(index)
    if index < 1 or index > self.length + 1 then
        print("索引越界")
        return
    end
end

function Linkedlist:CheckAll(index)
    Linkedlist:CheckList()
    Linkedlist:CheckIndex(index)
end

-- 尾部添加节点
function Linkedlist:AddTail(data)
    Linkedlist:CheckList()
    local node = self
    while node.next do
        node = node.next
    end
    -- 创建新的尾部节点
    node.next = {}
    node = node.next
    node.next = nil
    node.data = data
    self.length = self.length+1
    print("尾部添加节点成功")
end

-- 头部添加节点
function Linkedlist:AddHead(data)
    Linkedlist:CheckList()
    --修改新节点和头节点的next
    local newNode = {}
    newNode.data = data
    newNode.next = self.next
    self.next = newNode
    self.length = self.length+1
    print("头部添加节点成功")
end

-- 索引添加节点
function Linkedlist:InsertByIndex(index, data)
    Linkedlist:CheckAll(index)
    local j,k,l = index - 1, 0, self
    while k ~= j do
        k = k + 1
        l = l.next
    end
    -- 开始添加
    local newNode = {}
    newNode.data = data
    newNode.next = l.next
    l.next = newNode
    self.length = self.length + 1
    print("索引添加节点成功")
end

-- 索引删除并返回节点数据内容
function Linkedlist:DeleteByIndex(index)
    Linkedlist:CheckAll(index)
    local j,k,l = index - 1, 0, self
    while k ~= j do
        k = k + 1
        l = l.next
    end
    -- 开始删除 l.next
    d = l.next.data
    t = l.next.next
    l.next = nil
    l.next = t
    self.length = self.length - 1
    print("索引删除节点成功")
end

-- 索引修改节点内容
function Linkedlist:ModifyDataByIndex(index, data)
    Linkedlist:CheckAll(index)
    local l = self.next
    local k = 1
    while l do
        if k == index then
            l.data = data
            print("索引修改节点成功")
            return
        end
        l = l.next
        k = k + 1
    end
    print("索引越界")
end

-- 索引获取指定节点内容
function Linkedlist:GetDataByIndex(index)
    Linkedlist:CheckAll(index)
    local l = self.next
    local k = 1
    while l do
        if k == index then
            print("索引获取节点成功")
            return l.data
        end
        l = l.next
        k = k + 1
    end
    print("索引越界")
end

-- 反转倒置链表
function Linkedlist:Reverse()
    Linkedlist:CheckList()
    -- p为当前遍历的节点 q为前遍历节点的前一节点 pr中间变量
    -- ret 返回反转后的链表
    local p, q= self.next, nil
    self.next = nil
    while p do
        pr = p.next
        p.next = q
        q = p
        p = pr
    end
    self.next = q
    return self
end

-- 打印链表元素
function Linkedlist:Display()
    Linkedlist:CheckList()
    local l = self.next
    local x = 1
    while l do
        print(x .. " : " .. l.data)
        l = l.next
        x = x + 1
    end
end

-- 链表是否为空
function Linkedlist:IsEmpty()
    Linkedlist:CheckList()
    return self.length == 0
end

-- 清理链表，操作完成后，链表还在，只不过为空，相当刚开始的初始化状态
function Linkedlist:Clear()
    Linkedlist:CheckList()
    while true do
        -- fNode为遍历用指针
        fNode = self.next
        -- 满足则已为空表
        if not fNode then
            print("链表清理成功")
            break
        end
        t = fNode.next
        -- 置空
        self.next = nil
        self.next = t
    end
    list.length = 0
end

-- 销毁链表
function Linkedlist:Destory()
    if self then
        Linkedlist:Clear()
    end
    self = nil
    print("链表销毁成功")
end

return Linkedlist