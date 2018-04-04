
Before you start:

----------------------------------------------------------------
1. Employee table population to test cache


--populate Employees table
BEGIN
  FOR icount IN 300..5000 LOOP
    INSERT INTO employees VALUES 
        ( icount
        , 'Janette'
        , 'King'
        , 'JKING'||icount
        , '011.44.1345.429268'
        , TO_DATE('30-01-2004', 'dd-MM-yyyy')
        , 'SA_REP'
        , 10000
        , .35
        , 146
        , 80
        );
  END LOOP;
END;


-- Using Result Cache

select /*+ result_cache */* from employees




---------------------------------------------------------------------------------------------
2.Create table add autoincrement

create table Product ( 
ID number(10),
NAME varchar2(150),
MODEL varchar2(100),
PRICE number(10),
constraint pk_product_id PRIMARY KEY(ID)
);


CREATE SEQUENCE Product_seq;


CREATE OR REPLACE TRIGGER Product_trigger
BEFORE INSERT ON Product
FOR EACH ROW
   BEGIN
     SELECT Product_seq.nextval INTO :new.id FROM dual;
   END;
   
   

3.Store procedures
	1.
   --  Procedure that inserts a new record into the 'Product' table.
   --
  create or replace PROCEDURE add_product (
      p_name            varchar2,
      p_model         VARCHAR2,
      p_price           number
   )
   AS
   BEGIN
      INSERT INTO Product(name, model, price)
         VALUES(p_name, p_model, p_price);
   END;






   2.
   --Procedure to select Departments biggest salary
  
create or replace procedure departments_biggest_salary
  (
	CURSOR_  OUT SYS_REFCURSOR
  )
  as
  Begin
   OPEN CURSOR_ FOR
	select /*+ result_cache */ departments.department_name,t.Highest_salary from departments
	inner join  ( select department_id, MAX(salary) AS Highest_salary
	from employees
	group by department_id) t
	on departments.department_id =t.department_id;
  End;