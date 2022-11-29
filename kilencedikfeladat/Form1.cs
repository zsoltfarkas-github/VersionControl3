﻿using kilencedikfeladat.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kilencedikfeladat
{
    public partial class Form1 : Form
    {

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();

        List<int> male = new List<int>();
        List<int> female = new List<int>();

        Random rng = new Random(1234);

        public Form1()
        {
            InitializeComponent();

            

        }

        private void Simulation()
        {
            for (int year = 2005; year <= numericUpDown1.Value; year++)
            {

                for (int i = 0; i < Population.Count; i++)
                {
                    SimStep(year, Population[i]);
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                int j = 0;
                int evszam = 2005;
                male[j] = nbrOfMales;
                female[j] = nbrOfFemales;
                evszam++;
                j++;
            }

            

        }

        private void SimStep(int year, Person person)
            {
                if (!person.IsAlive) return;

                byte age = (byte)(year - person.BirthYear);

                double pDeath = (from x in DeathProbabilities
                                 where x.Gender == person.Gender && x.Kor == age
                                 select x.P).FirstOrDefault();

                if (rng.NextDouble() <= pDeath)
                    person.IsAlive = false;

                if (person.IsAlive && person.Gender == Gender.Female)
                {

                    double pBirth = (from x in BirthProbabilities
                                     where x.Kor == age
                                     select x.P).FirstOrDefault();

                    if (rng.NextDouble() <= pBirth)
                    {
                        Person újszülött = new Person();
                        újszülött.BirthYear = year;
                        újszülött.NbrOfChildren = 0;
                        újszülött.Gender = (Gender)(rng.Next(1, 3));
                        Population.Add(újszülött);
                    }
                }

            
            }

        public List<Person> GetPopulation(string path)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(textBox1.Text.ToString(), Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> getbirthprobabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    getbirthprobabilities.Add(new BirthProbability()
                    {
                        Kor = int.Parse(line[0]),
                        NbrofChildren = int.Parse(line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return getbirthprobabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> getdeathprobabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    getdeathprobabilities.Add(new DeathProbability()
                    {
                        Kor = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        P = double.Parse(line[2])
                    });
                }
            }

            return getdeathprobabilities;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            male.Clear();
            female.Clear();
            richTextBox1.Clear();
            Population = GetPopulation(textBox1.Text.ToString());
            BirthProbabilities = GetBirthProbabilities(@"C:\Windows\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Windows\Temp\halál.csv");
            Simulation();
            DisplayResults();
        }

        private void DisplayResults()
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = Path.GetFullPath(@"C:\Windows\Temp\nép.csv");
                textBox1.Text = path;
            }
        }
    }
}
