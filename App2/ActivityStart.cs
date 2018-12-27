using System;
using System.Collections.Generic;using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace App2
{
    [Activity(Label = "ActivityStart")]
    public class ActivityStart : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout3);

            FindViewById<Button>(Resource.Id.btnLog).Click += btnClick_Logout;

            string day = GetDay(DateTime.Now.DayOfWeek);
            FindViewById<TextView>(Resource.Id.txtHandle).Text = day + " schedule";
            StudentUX_Day_Schedule((int)DateTime.Now.DayOfWeek);
        }

        private void StudentUX_Day_Schedule(int dayOfWeek)
        {
            var response = new WebClient().DownloadString("http://192.168.1.241:57272/api/getSchedule/1");
            int t = 10;
        }

        private string GetDay(DayOfWeek dayOfWeek)
        {
            return dayOfWeek.ToString();
        }

        private void btnClick_Logout(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(LoginActivity)));
        }
    }
}


//foreach (var user in Unit.StudentsRepository.AllItems)
//{
//    if (i == 0)
//    {
//        user1 = user;
//        i++;
//    }
//}
//var stud =
//    (from l in Unit.AudLectRepository.AllItems
//     join g in Unit.GroupsRepository.AllItems
//     on l.Group.Id equals g.Id
//     join ts in Unit.LectionsRepository.AllItems
//     on l.LectId equals ts.Id
//     join a in Unit.AudiencesRepository.AllItems
//     on l.AudId equals a.Id
//     join teas in Unit.TeachSubjRepository.AllItems
//     on l.TeachSubj.Id equals teas.Id
//     join teachers in Unit.TeachersRepository.AllItems
//     on teas.TeacherId equals teachers.Id
//     join ss in Unit.SubjectsRepository.AllItems
//     on teas.SubjId equals ss.Id
//     select new
//     {
//         l.GroupId,
//         Group = g.Name,
//         ts.Day,
//         ts.Start,
//         ts.Finish,
//         Audience = a.Name,
//         Teacher = teachers.LastName + " " + teachers.FirstName,
//         Subject = ss.Name

//     }).ToList();

//var _group = user1.Group.Id;

//        //GridAdd(1, "group");
//        //GridAdd(2, "day");
//        //GridAdd(2, "start");
//        //GridAdd(2, "end");
//        //GridAdd(2, "Auditory");
//        //GridAdd(2, "Teacher");
//        //GridAdd(2, "Subject");

//        foreach (var lect in stud)
//        {
//            if (lect.GroupId == _group)
//                InfoDataGridView.Rows.Add("", lect.Group, lect.Day,
//                    $"{lect.Start.Hour}:{lect.Start.Minute}",
//                    $"{lect.Finish.Hour}:{lect.Finish.Minute}",
//                    lect.Audience, lect.Teacher, lect.Subject);
//        }
//    }
//        else if (cell_CB.EditedFormattedValue.ToString().Equals("Группа"))
//        {
//            InfoDataGridView.Columns.Add("FirstName", "FirstName");
//            InfoDataGridView.Columns.Add("LastName", "LastName");
//            InfoDataGridView.Columns.Add("Birthday", "Birthday");
//            InfoDataGridView.Columns.Add("LogBook", "LogBook");
//            InfoDataGridView.Columns.Add("Email", "Email");
//            InfoDataGridView.Columns.Add("Address", "Address");
//            InfoDataGridView.Columns.Add("Phone", "Phone");
//            int id = user1.Id;
//    string groupId = null;

//    var stud = (from p in Unit.PhonesRepository.AllItems
//                join s in Unit.StudentsRepository.AllItems
//                on p.Student.Id equals s.Id
//                join g in Unit.GroupsRepository.AllItems
//                on s.Group.Id equals g.Id
//                select new
//                {
//                    s.Id,
//                    s.FirstName,
//                    s.LastName,
//                    Logbook = s.LogbookNumber,
//                    Number = p.StudentsPhone,
//                    s.Birthday,
//                    s.Email,
//                    s.Address,
//                    Group = g.Name
//                }).ToList();

//            foreach (var st in stud)
//                if (st.Id == id)
//                    groupId = st.Group;

//            foreach (var st in stud)
//                if (st.Group == groupId)
//                    InfoDataGridView.Rows.Add("", st.FirstName, st.LastName,
//                        st.Birthday.ToString("dd/MM/yyyy"),
//                        st.Logbook, st.Email, st.Address, st.Number);
//        }
//        else if (cell_CB.EditedFormattedValue.ToString().Equals("Оценки"))
//        {
//            int id = user1.Id;
//InfoDataGridView.Columns.Add("Mark", "Mark");
//            InfoDataGridView.Columns.Add("Subj", "Subj");
//            InfoDataGridView.Columns.Add("Teacher", "Teacher");

//            var stud = (from m in Unit.MarksRepository.AllItems
//                        join s in Unit.StudentsRepository.AllItems
//                        on m.Student.Id equals s.Id
//                        join ts in Unit.TeachSubjRepository.AllItems
//                        on m.TeachSubj.Id equals ts.Id
//                        join teachers in Unit.TeachersRepository.AllItems
//                        on ts.TeacherId equals teachers.Id
//                        join ss in Unit.SubjectsRepository.AllItems
//                        on ts.SubjId equals ss.Id
//                        select new
//                        {
//                            s.Id,
//                            m.StudentsMark,
//                            teachers.LastName,
//                            ss.Name
//                        }).ToList();

//            foreach (var mark in stud)
//                if (mark.Id == id)
//                    InfoDataGridView.Rows.Add("", mark.StudentsMark, mark.Name, mark.LastName);
//        }