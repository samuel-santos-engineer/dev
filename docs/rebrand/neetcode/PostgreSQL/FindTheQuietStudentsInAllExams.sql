-- Find the Quiet Students in All Exams
with T0 as (
    select exam_id, min(score) mn, max(score) mx
    from exam
    group by exam_id
),
T1 as (
    select student_id
    from exam e
    inner join T0 on e.score = T0.mn and e.exam_id = T0.exam_id
    union
    select student_id
    from exam e
    inner join T0 on e.score = T0.mx and e.exam_id = T0.exam_id
),
T2 as (
    select distinct student_id
    from exam e
    where e.student_id not in (select student_id from T1)
)
select t.* from T2
inner join student t on t.student_id = T2.student_id