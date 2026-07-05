-- Team Scores in Football Tournament
with T0 as (
    select host_team team_id, host_goals team_goals, guest_goals op_goals
    from matches
    union all
    select guest_team team_id, guest_goals team_goals, host_goals op_goals
    from matches
),
T1 as (
    select team_id, case when team_goals > op_goals then 3 else case when team_goals = op_goals then 1 else 0 end end num_points
    from T0
),
T2 as (
    select team_id, sum(num_points) num_points
    from T1
    group by team_id
)
select t.team_id, t.team_name, case when T2.num_points is null then 0 else T2.num_points end num_points from teams t
left join T2 on t.team_id = T2.team_id
order by num_points desc, team_id asc;