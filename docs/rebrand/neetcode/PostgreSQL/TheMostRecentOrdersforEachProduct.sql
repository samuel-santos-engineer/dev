-- The Most Recent Orders for Each Product
with T0 as (
select product_id, max(order_date) recent_date
from orders
group by product_id
),
T1 as (
    select t1.order_id, t1.product_id, t1.order_date
    from orders t1
    inner join T0 on t1.product_id = t0.product_id and t1.order_date = t0.recent_date
)
select p.product_name, T1.product_id, T1.order_id, T1.order_date
from T1
inner join products p on T1.product_id = p.product_id
order by p.product_name asc, T1.product_id asc, T1.order_id asc
;