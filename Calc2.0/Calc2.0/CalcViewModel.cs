using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.FormsBook.Toolkit;

namespace Calc2._0
{
    public class CalcViewModel : ViewModelBase
    {
        string currentEntry = "0";
        string historyString = "";
        bool isSumDisplayed = false;
        double accumulatedSum = 0;
        string second = "10ˣ";
        string secondLabel = "#212121";
        int op = -1; //1+,2-,3*,4/

        public CalcViewModel()
        {
            ClearCommand = new Command(
                execute: () =>
                {
                    HistoryString = "";
                    accumulatedSum = 0;
                    CurrentEntry = "0";
                    isSumDisplayed = false;
                    RefreshCanExecutes();
                });

            ClearEntryCommand = new Command(
                execute: () =>
                {
                    CurrentEntry = "0";
                    isSumDisplayed = false;
                    RefreshCanExecutes();
                });

            BackspaceCommand = new Command(
                execute: () =>
                {
                    CurrentEntry = CurrentEntry.Substring(0, CurrentEntry.Length - 1);

                    if (CurrentEntry.Length == 0)
                    {
                        CurrentEntry = "0";
                    }

                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isSumDisplayed && (CurrentEntry.Length > 1 || CurrentEntry[0] != '0');
                });

            NumericCommand = new Command<string>(
                execute: (string parameter) =>
                {
                    if (isSumDisplayed || CurrentEntry == "0")
                        CurrentEntry = parameter;
                    else
                        CurrentEntry += parameter;

                    isSumDisplayed = false;
                    RefreshCanExecutes();
                },
                canExecute: (string parameter) =>
                {
                    return isSumDisplayed || CurrentEntry.Length < 16;
                });

            DecimalPointCommand = new Command(
                execute: () =>
                {
                    if (isSumDisplayed)
                        CurrentEntry = "0.";
                    else
                        CurrentEntry += ".";

                    isSumDisplayed = false;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return isSumDisplayed || !CurrentEntry.Contains(".");
                });

            AddCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    accumulatedSum += value;
                    CurrentEntry = "0";
                    op = 1;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });

            SubCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (accumulatedSum != 0)
                        accumulatedSum -= value;
                    else
                        accumulatedSum = value;
                    CurrentEntry = "0";
                    op = 2;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            MulCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (accumulatedSum != 0)
                        accumulatedSum *= value;
                    else
                        accumulatedSum = value;
                    CurrentEntry = "0";
                    op = 3;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            DivCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (accumulatedSum == 0)
                        accumulatedSum = value;
                    else
                    {
                        if (value == 0)
                            accumulatedSum = 0;
                        else
                            accumulatedSum /= value;
                    }
                    CurrentEntry = "0";
                    op = 4;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            SumCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (op == 1)
                        accumulatedSum += value;
                    else if (op == 2)
                        accumulatedSum += (-value);
                    else if (op == 3)
                        accumulatedSum *= value;
                    else if (op == 4)
                    {
                        if (value == 0)
                            accumulatedSum = 0;
                        else
                            accumulatedSum /= value;
                    }
                    CurrentEntry = accumulatedSum.ToString();
                    accumulatedSum = 0;
                    op = -1;
                    isSumDisplayed = true;
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !isSumDisplayed;
                });

            FactCommand = new Command(
                execute: () =>
                {
                    int N = Int32.Parse(CurrentEntry);
                    int fact = 1;
                    for (int i = 1; i <= N; i++)
                        fact *= i;
                    CurrentEntry = fact.ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return !CurrentEntry.Contains(".") && Double.Parse(CurrentEntry) >= 0;
                });
            PlusMinusCommand = new Command(
                execute: () =>
                {
                    double v = -Double.Parse(CurrentEntry);
                    CurrentEntry = v.ToString();
                    RefreshCanExecutes();
                });
            SqrCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (value * value).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            OneOverCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (1 / value).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) != 0;
                });
            SqrtCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (Math.Sqrt(value)).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) >= 0;
                });
            cubertCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (Math.Pow(value, (double) 1/3)).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) >= 0;
                });
            lnCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = Math.Log(value).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) != 0;
                });
            log10Command = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = Math.Log10(value).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) != 0;
                });
            exCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = Math.Exp(value).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            sinCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (Math.Sin(value)).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            cosCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (Math.Cos(value)).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            tanCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    CurrentEntry = (Math.Tan(value)).ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return true;
                });
            randCommand = new Command(
                execute: () =>
                {
                    Random rand = new System.Random();
                    CurrentEntry = rand.NextDouble().ToString();
                    RefreshCanExecutes();
                },
                canExecute: () =>
                {
                    return Double.Parse(CurrentEntry) == 0;
                });
            secondCommand = new Command(
                execute: () =>
                {
                    if (Second == "10ˣ")
                    {
                        Second = "2ˣ";
                        SecondLabel = "#212121";
                    }
                    else
                    {
                        Second = "10ˣ";
                        SecondLabel = "#FF0000";
                    }
                });
            tenxCommand = new Command(
                execute: () =>
                {
                    double value = Double.Parse(CurrentEntry);
                    if (Second == "10ˣ")
                        CurrentEntry = (Math.Pow(10,value)).ToString();
                    else
                        CurrentEntry = (Math.Pow(2, value)).ToString();
                });

        }

        void RefreshCanExecutes()
        {
            ((Command)BackspaceCommand).ChangeCanExecute();
            ((Command)NumericCommand).ChangeCanExecute();
            ((Command)DecimalPointCommand).ChangeCanExecute();
            ((Command)AddCommand).ChangeCanExecute();
            ((Command)FactCommand).ChangeCanExecute();
            ((Command)SumCommand).ChangeCanExecute();
            ((Command)SubCommand).ChangeCanExecute();
            ((Command)MulCommand).ChangeCanExecute();
            ((Command)DivCommand).ChangeCanExecute();
            ((Command)SqrCommand).ChangeCanExecute();
            ((Command)SqrtCommand).ChangeCanExecute();
            ((Command)OneOverCommand).ChangeCanExecute();
            ((Command)cubertCommand).ChangeCanExecute();
            ((Command)lnCommand).ChangeCanExecute();
            ((Command)log10Command).ChangeCanExecute();
            ((Command)exCommand).ChangeCanExecute();
            ((Command)sinCommand).ChangeCanExecute();
            ((Command)cosCommand).ChangeCanExecute();
            ((Command)tanCommand).ChangeCanExecute();
            ((Command)randCommand).ChangeCanExecute();
            ((Command)tenxCommand).ChangeCanExecute();
        }

        public string CurrentEntry
        {
            private set { SetProperty(ref currentEntry, value); }
            get { return currentEntry; }
        }

        public string Second
        {
            private set { SetProperty(ref second, value); }
            get { return second; }
        }

        public string SecondLabel
        {
            private set { SetProperty(ref secondLabel, value); }
            get { return secondLabel; }
        }

        public string HistoryString
        {
            private set { SetProperty(ref historyString, value); }
            get { return historyString; }
        }

        public ICommand ClearCommand { private set; get; }
        public ICommand SumCommand { private set; get; }
        public ICommand SubCommand { private set; get; }
        public ICommand MulCommand { private set; get; }
        public ICommand DivCommand { private set; get; }
        public ICommand SqrCommand { private set; get; }
        public ICommand SqrtCommand { private set; get; }
        public ICommand OneOverCommand { private set; get; }
        public ICommand cubertCommand { private set; get; }
        public ICommand lnCommand { private set; get; }
        public ICommand log10Command { private set; get; }
        public ICommand exCommand { private set; get; }
        public ICommand sinCommand { private set; get; }
        public ICommand cosCommand { private set; get; }
        public ICommand tanCommand { private set; get; }
        public ICommand randCommand { private set; get; }
        public ICommand secondCommand { private set; get; }
        public ICommand tenxCommand { private set; get; }



        public ICommand ClearEntryCommand { private set; get; }

        public ICommand BackspaceCommand { private set; get; }

        public ICommand NumericCommand { private set; get; }
        public ICommand PlusMinusCommand { private set; get; }

        public ICommand DecimalPointCommand { private set; get; }
        public ICommand FactCommand { private set; get; }

        public ICommand AddCommand { private set; get; }

        public void SaveState(IDictionary<string, object> dictionary)
        {
            dictionary["CurrentEntry"] = CurrentEntry;
            dictionary["HistoryString"] = HistoryString;
            dictionary["isSumDisplayed"] = isSumDisplayed;
            dictionary["accumulatedSum"] = accumulatedSum;
        }

        public void RestoreState(IDictionary<string, object> dictionary)
        {
            CurrentEntry = GetDictionaryEntry(dictionary, "CurrentEntry", "0");
            HistoryString = GetDictionaryEntry(dictionary, "HistoryString", "");
            isSumDisplayed = GetDictionaryEntry(dictionary, "isSumDisplayed", false);
            accumulatedSum = GetDictionaryEntry(dictionary, "accumulatedSum", 0.0);

            RefreshCanExecutes();
        }

        public T GetDictionaryEntry<T>(IDictionary<string, object> dictionary,
                                        string key, T defaultValue)
        {
            if (dictionary.ContainsKey(key))
                return (T)dictionary[key];

            return defaultValue;
        }
    }
}
