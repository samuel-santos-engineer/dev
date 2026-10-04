public class LRUCache {

    class CacheLink
    {
        public int key;
        public int value;
        public CacheLink prev;
        public CacheLink next;
    }

    Dictionary<int, CacheLink> cache = null;
    CacheLink head = null;
    CacheLink tail = null;

    int capacity = 0;

    public LRUCache(int capacity) {
        this.capacity = capacity;
        this.cache = new Dictionary<int, CacheLink>();
        //dummy helper nodes
        this.head = new CacheLink();
        this.tail = new CacheLink();
        head.next = tail;
        tail.prev = head;
    }
    
    public int Get(int key) {
        //Console.WriteLine($"get:k={key}");
        if (!cache.TryGetValue(key, out CacheLink link)) return -1;

        MoveToFirstPosition(link);

        return link.value;
    }
    
    public void Put(int key, int value) {
        //Console.WriteLine($"put:k={key};v={value}");
        if (cache.TryGetValue(key, out CacheLink link))
        {
            link.value = value;
            MoveToFirstPosition(link);
        }
        else
        {
            if (cache.Count == capacity)
            {
                //remove tail
                CacheLink leastRecentUsed = tail.prev;
                Remove(leastRecentUsed);
                cache.Remove(leastRecentUsed.key);

            }
            link = new CacheLink()
            {
                key = key,
                value = value
            };
            cache.Add(key, link);
            AddFirst(link);
        }
        //Console.WriteLine($"put:k={PrintLink(link)}:linkHead:{PrintLink(linkHead)}:linkTail:{PrintLink(linkTail)}");
    }

    void MoveToFirstPosition(CacheLink link)
    {
        Remove(link);
        AddFirst(link);
    }

    void Remove(CacheLink link)
    {
        link.prev.next = link.next;
        link.next.prev = link.prev;
    }

    void AddFirst(CacheLink link)
    {
        link.next = head.next;
        link.prev = head;
        head.next.prev = link;
        head.next = link;
    }

    static string PrintLink(CacheLink link)
    {
        return $"{link.key}[p:{(link.prev == null ? "n" : link.prev.key)},n:{(link.next == null ? "n" : link.next.key)}]";
    }
}

/**
 * Your LRUCache object will be instantiated and called as such:
 * LRUCache obj = new LRUCache(capacity);
 * int param_1 = obj.Get(key);
 * obj.Put(key,value);
 */