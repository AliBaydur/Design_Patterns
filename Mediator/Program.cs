﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator =new Mediator();
            

            Teacher ali = new Teacher(mediator);
            ali.Name = "Ali";

            mediator.Teacher = ali;

            Student omer =new Student(mediator);
            omer.Name = "Ömer";

            

            Student yusuf = new Student(mediator);
            yusuf.Name = "Yusuf";

            mediator.Students = new List<Student>{omer,yusuf};

            ali.SendNewImageUrl("slide1.jpg");
            ali.RecieveQuestion("is it true?",yusuf);

            Console.ReadLine();
        }
    }

    abstract class CourseMember
    {
        protected Mediator Mediator;

        protected CourseMember(Mediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Teacher:CourseMember
    {
        public Teacher(Mediator mediator) : base(mediator)
        {
        }

        public string Name { get; set; }

        public void RecieveQuestion(string question, Student student)
        {
            Console.WriteLine("Teacher recieved a question from {0},{1}",student.Name,question);
        }

        public void SendNewImageUrl(string url)
        {
            Console.WriteLine("Teacher changed slide : {0}",url);
            Mediator.UpdateImage(url);
        }

        public void AnswerQuestion(string answer,Student student)
        {
            Console.WriteLine("Teacher answered question {0},{1}",student.Name,answer);
        }
        
    }

    class Student:CourseMember
    {
        public Student(Mediator mediator) : base(mediator)
        {
        }

        public void RecieveImage(string url)
        {
            Console.WriteLine("{1} received image : {0}",url,Name);
        }

        public void ReceiveAnswer(string answer)
        {
            Console.WriteLine("Student received answer {0}",answer);
        }

        public string Name { get; set; }

        
    }

    class Mediator
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UpdateImage(string url)
        {
            foreach (var student in Students)
            {
                student.RecieveImage(url);
            }
        }

        public void SendQuestion(string question, Student student)
        {
            Teacher.RecieveQuestion(question,student);
        }

        public void SendAnswer(string answer,Student student)
        {
            student.ReceiveAnswer(answer);
        }
    }
}
