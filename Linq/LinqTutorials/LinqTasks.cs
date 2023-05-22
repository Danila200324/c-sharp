using LinqTutorials.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqTutorials
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts

            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
                     var result = Emps.Where(a => a.Job == "Backend programmer");
                     
                     var result1 =
                         from p in Emps
                         where p.Job == "Backend programmer"
                         select p;
            
                     return result;
        }

        
        

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            IEnumerable<Emp> result = Emps.Where(a => a.Job == "Frontend programmer" &&
                                                      a.Salary > 1000).OrderByDescending(a => a.Ename);
            IEnumerable<Emp> result1 =
                from p in Emps
                where p.Job == "Frontend programmer" && p.Salary > 1000
                orderby p.Ename descending
                select p;
            return result1;
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            int result = Emps.Max(a => a.Salary);

            int result1 =
                (from p in Emps
                select p.Salary).Max();
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            IEnumerable<Emp> result = Emps.Where(a => a.Salary == Emps.Max(b => b.Salary));

            IEnumerable<Emp> result1 =
                from p in Emps
                where p.Salary == (from q in Emps select q.Salary).Max()
                select p;
            return result1;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            IEnumerable<object> result = Emps.Select(emp => new { Nazwisko = emp.Ename, Praca = emp.Job });
            
            IEnumerable<object> result1 = from p in Emps
                    select new
                    {
                        Nazwisko = p.Ename,
                        Praca = p.Job
                    };
            return result;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     Result: Conjuction of Emp and Dept collections
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            IEnumerable<object> result = Emps.Join(Depts,
                emp => emp.Deptno,
                dept => dept.Deptno,
                (emp, dept) => new { emp.Ename, emp.Job, dept.Dname});
            
            IEnumerable<object> result1 = from emp in Emps
                join dept in Depts on emp.Deptno equals dept.Deptno
                select new { emp.Ename, emp.Job, dept.Dname };
                
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            IEnumerable<object> result =
                from emp in Emps
                group emp by emp.Job
                into groupedEmps
                select new { Praca = groupedEmps.Key, LiczbaPracownikow = groupedEmps.Count() };

            IEnumerable<object> result1 = Emps.GroupBy(emp => emp.Job).Select(groupedEmp =>
                new { Praca = groupedEmp.Key, LiczbaPracownikow = groupedEmp.Count() });
            
            return result1;
        }

        /// <summary>
        ///     Returns "true" if at least one
        ///     of the elements of collection works
        ///     as "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            bool result = Emps.Any(emp => emp.Job =="Backend programmer");
            bool result1 = (from emp in Emps
                where emp.Job == "Backend programmer"
                select emp).Any();
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            Emp result = Emps.Where(emp => emp.Job == "Frontend programmer").
                OrderByDescending(emp => emp.HireDate).First();

            Emp result1 =
                (from p in Emps
                    where p.Job == "Frontend programmer"
                    orderby p.HireDate descending
                    select p).First();
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            
            var query = (from emp in Emps
                    select new { emp.Ename, emp.Job, emp.HireDate })
                .Union<object>(
                    from empty in Enumerable.Range(0, 1)
                    select new { Ename = "Brak wartości", Job = (string)null, Hiredate = (DateTime?)null });

            var result1 = Emps.Select(emp => new { emp.Ename, emp.Job, emp.HireDate })
                .Union<object>(Enumerable.Repeat(new { Ename = "Brak wartości", Job = (string)null, Hiredate = (DateTime?)null }, 1));

            return result1;
        }

        /// <summary>
        /// Using LINQ get all employees sorted by departments. Remember that:
        /// 1. We are only considering departments with count of emps >= 1
        /// 2. We want to return list of objects of following structure:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Use anonymous types
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            IEnumerable<object> result =
                from emp in Emps
                group emp by emp.Deptno
                into groupedEmps
                where groupedEmps.Count() >= 1
                join dept in Depts on groupedEmps.Key equals dept.Deptno 
                select new
                {
                   name = dept.Dname ,
                   numOfEmployees = groupedEmps.Count()
                };

                IEnumerable<object> result1 = Depts.GroupJoin(Emps,
                    dept => dept.Deptno,
                    emp => emp.Deptno,
                    (dept, empCollection) => new
                    {
                        name = dept.Dname,
                        numOfEmployees = empCollection.Count()
                    }).Where(x => x.numOfEmployees >= 1);
            return result;
        }

        /// <summary>
        /// Write your own extension method, that compiles the following code.
        /// Add this method to the CustomExtensionMethods defined below.
        /// Method should return only those employees, who have at least 1 direct subordinate.
        /// Employees should be ordered by surname (asc) and salary (desc)
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            
            IEnumerable<Emp> result = Emps.GetEmpsWithSubordinates();

            IEnumerable<Emp> result1 = 
                from p in Emps.GetEmpsWithSubordinates()
                select p;
            return result1;
        }

        /// <summary>
        /// Following method should return single int number.
        /// As input we take list of integers/
        /// Try using LINQ to find all the numbers that appearance count is odd.
        /// We assume there's always at least one of those numbers.
        /// E.G.: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static IEnumerable<int> Task13(int[] arr)
        {
            IEnumerable<int> result1 = from p in arr
                group p by p
                into groupedSet
                where groupedSet.Count() % 2 != 0
                select groupedSet.Key;

            IEnumerable<int> result = arr.GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count())
                .Where(x => x.Value % 2 != 0)
                .Select(kv => kv.Key);
            return result1;
        }

        /// <summary>
        /// Return only the departments that have at least 5 employees or they don't have any.
        /// Order the results by the name of the department (asc)
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            IEnumerable<Dept> result = from d in Depts
                join e in Emps on d.Deptno equals e.Deptno into empGroup
                where empGroup.Count() >= 5 || empGroup.Count() == 0
                orderby d.Dname 
                select d;
            
            var result1 = Depts.Where(d => Emps.Count(e => e.Deptno == d.Deptno) >= 5 || !Emps.Any(e => e.Deptno == d.Deptno))
                .OrderBy(d => d.Dname);
            
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        //Put your extension methods here
        public static IEnumerable<Emp> GetEmpsWithSubordinates(this IEnumerable<Emp> emps)
        {
            var result = emps.Where(e => emps.Any(e2 => e2.Mgr == e.Mgr)).OrderBy(e => e.Ename).ThenByDescending(e => e.Salary);
            return result;
        }

    }
}
