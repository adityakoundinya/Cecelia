using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cecelia {

    class Program {
        static bool isCfOnly = true;
        static bool isSfOnly = true;

        static void Main(string[] args) {
            List<Product> unorderedProducts = new List<Product>();
            CeceliaDataProvider dp = new CeceliaDataProvider();
            StringBuilder text = new StringBuilder();

            unorderedProducts = dp.GetAllProducts();

            if (isCfOnly) {
                unorderedProducts.RemoveAll(o => o.CF == false);
            }
            if (isSfOnly) {
                unorderedProducts.RemoveAll(o => o.SF == false);
            }

            List<Category> Products = LoadProducts(unorderedProducts);

            WriteToTextFile(Products);
        }

        private static List<Category> LoadProducts(List<Product> unorderedProducts) {
            List<Category> Products = new List<Category>();
            foreach (Product p in unorderedProducts) {
                if (!Products.Exists(o => o.CategoryName == p.Category)) {

                    if (p.Type2 != string.Empty) {

                        Type1 t1 = BuildType1withType2(p);
                        Company comp = BuildCompanywithType1(p, t1);
                        Category cat = BuildCategory(p, comp);
                        Products.Add(cat);

                    } else if (p.Type1 != string.Empty) {

                        Type1 t1 = BuildType1withFlavor(p);
                        Company comp = BuildCompanywithType1(p, t1);
                        Category cat = BuildCategory(p, comp);
                        Products.Add(cat);

                    } else if (p.CompanyName != string.Empty) {

                        Company comp = BuildCompanywithFlavor(p);
                        Category cat = BuildCategory(p, comp);
                        Products.Add(cat);

                    }

                } else {
                    Category cat = Products.Find(o => o.CategoryName == p.Category);

                    if (!cat.CompanyList.Exists(o => o.CompanyName == p.CompanyName)) {

                        if (p.Type2 != string.Empty) {

                            Type1 t1 = BuildType1withType2(p);
                            Company comp = BuildCompanywithType1(p, t1);
                            cat.CompanyList.Add(comp);

                        } else if (p.Type1 != string.Empty) {

                            Type1 t1 = BuildType1withFlavor(p);
                            Company comp = BuildCompanywithType1(p, t1);
                            cat.CompanyList.Add(comp);

                        } else {

                            Company comp = BuildCompanywithFlavor(p);
                            cat.CompanyList.Add(comp);
                        }

                    } else {
                        Company comp = cat.CompanyList.Find(o => o.CompanyName == p.CompanyName);

                        if (p.Type1 != string.Empty) {
                            if (!comp.Type1List.Exists(o => o.Type1Name == p.Type1)) {
                                if (p.Type2 != string.Empty) {

                                    Type1 t1 = BuildType1withType2(p);
                                    comp.Type1List.Add(t1);

                                } else {

                                    Type1 t1 = BuildType1withFlavor(p);
                                    comp.Type1List.Add(t1);

                                }
                            } else {
                                Type1 type1 = comp.Type1List.Find(o => o.Type1Name == p.Type1);

                                if (p.Type2 != string.Empty) {
                                    if (!type1.Type2List.Exists(o => o.Type2Name == p.Type2)) {
                                        if (p.Type2 != string.Empty) {

                                            Type2 t2 = BuildType2(p);
                                            type1.Type2List.Add(t2);

                                        }
                                    } else {

                                        Type2 type2 = type1.Type2List.Find(o => o.Type2Name == p.Type2);

                                        Flavor f = BuildFlavor(p);
                                        type2.Type2Flavor.Add(f);

                                    }
                                } else {

                                    Flavor f = BuildFlavor(p);
                                    type1.Type1Flavor.Add(f);

                                }
                            }
                        } else {

                            Flavor f = BuildFlavor(p);
                            comp.CompanyFlavor.Add(f);

                        }
                    }
                }
            }
            return Products;
        }

        private static void WriteToTextFile(List<Category> Products) {
            TextWriter tw = new StreamWriter(GetPath());
            try {
                foreach (Category cat in Products) {
                    tw.WriteLine("");
                    tw.WriteLine("------------------------------------------------------------------------------");
                    tw.WriteLine(cat.CategoryName);
                    foreach (Company comp in cat.CompanyList) {
                        string compflav = string.Empty;
                        if (comp.IsFAC) {
                            compflav = comp.CompanyName + "* ";
                        } else {
                            compflav = comp.CompanyName;
                        }
                        if ((comp.CompanyFlavor != null && comp.CompanyFlavor.Count > 0) || (comp.Type1List != null && comp.Type1List.Count > 0)) {
                            if (comp.CompanyFlavor != null && comp.CompanyFlavor.Count == 1 && comp.CompanyFlavor[0].flavor == "") {
                            } else {
                                compflav += " - ";
                            }
                        }

                        compflav += OrderFlavorType(comp.CompanyFlavor, comp.Type1List);

                        tw.WriteLine(compflav);
                    }

                }
            } catch (Exception e) {
                tw.WriteLine(e.Message + " ----- " + e.StackTrace);
            } finally {
                tw.Flush();
                tw.Close();
            }
        }

        private static string GetPath() {
            string path = string.Empty;
            string location = @"C:\References\CeceliaArchive\CeceliaExtracts\";
            string fileName = "Cecelia";
            string ext = ".txt";

            string dateTime = DateTime.Now.ToShortDateString();
            dateTime = dateTime.Replace("/","_");
            string extractKind = string.Empty;
            if (isCfOnly && !isSfOnly) {
                extractKind = "CF";
            } else if (isSfOnly && !isCfOnly) {
                extractKind = "SF";
            } else if (isCfOnly && isSfOnly) {
                extractKind = "CF & SF";
            } else {
                extractKind = "GF";
            }
            path = location + fileName + "_" + extractKind + "_" + dateTime + ext;
            return path;
        }

        #region Loader Methods
        private static Category BuildCategory(Product p, Company comp) {
            Category cat = new Category();
            cat.CategoryName = p.Category;
            cat.CompanyList = new List<Company>() { comp };
            return cat;
        }

        private static Company BuildCompanywithFlavor(Product p) {
            Company comp = new Company();
            Flavor f = BuildFlavor(p);
            comp.CompanyFlavor = new List<Flavor>() { f };
            comp.IsFAC = p.FAC;
            comp.CompanyName = p.CompanyName;
            comp.Type1List = new List<Type1>();
            return comp;
        }

        private static Company BuildCompanywithType1(Product p, Type1 t1) {
            Company comp = new Company();
            comp.CompanyName = p.CompanyName;
            comp.CompanyFlavor = new List<Flavor>();
            comp.Type1List = new List<Type1>() { t1 };
            comp.IsFAC = p.FAC;
            return comp;
        }

        private static Type1 BuildType1withFlavor(Product p) {
            Type1 t1 = new Type1();
            t1.Type1Name = p.Type1;
            if (p.Flavor != string.Empty) {
                Flavor f = BuildFlavor(p);
                t1.Type1Flavor = new List<Flavor>() { f };
            } else {
                t1.Type1Flavor = new List<Flavor>();
            }
            t1.Type2List = new List<Type2>();
            return t1;
        }

        private static Type1 BuildType1withType2(Product p) {
            Type2 t2 = BuildType2(p);

            Type1 t1 = new Type1();
            t1.Type1Flavor = new List<Flavor>();
            t1.Type1Name = p.Type1;
            t1.Type2List = new List<Type2>() { t2 };
            return t1;
        }

        private static Flavor BuildFlavor(Product product) {
            Flavor f = new Flavor();
            f.IsCRT = product.CRT;
            if (f.IsCRT) {
                f.flavor = product.Flavor + "~";
            } else {
                f.flavor = product.Flavor;
            }

            return f;
        }

        private static Type2 BuildType2(Product p) {
            Type2 t2 = new Type2();
            if (p.Flavor != string.Empty) {
                Flavor f = BuildFlavor(p);
                t2.Type2Flavor = new List<Flavor>() { f };
            } else {
                t2.Type2Flavor = new List<Flavor>();
            }
            t2.Type2Name = p.Type2;
            return t2;
        }
        #endregion

        #region Printer Methods
        private static string OrderFlavorType(List<Flavor> flavors, List<Type1> typeflavor) {
            List<string> flavor = new List<string>();
            string printer = string.Empty;

            if (flavors.Count > 0) {
                foreach (Flavor f in flavors) {
                    flavor.Add(f.flavor);
                }
            }

            if (typeflavor.Count > 0) {
                foreach (Type1 t in typeflavor) {
                    flavor.Add(t.Type1Name);
                }
            }

            flavor.Sort();

            for (int i = 0; i < flavor.Count; i++) {
                if (flavors.Exists(o => o.flavor == flavor[i])) {
                    if (i == 0) {
                        printer += flavor[i];
                    } else {
                        printer += ", " + flavor[i];
                    }
                } else {
                    Type1 type1 = typeflavor.Find(o => o.Type1Name == flavor[i]);
                    if (i == 0) {
                        printer += type1.Type1Name;
                        if (type1.Type2List.Count > 0) {
                            if ((type1.Type2List.Count + type1.Type1Flavor.Count) > 1) {
                                printer += " (";
                            } else {
                                printer += " ";
                            }
                            printer += OrderType1Type2(type1.Type1Flavor, type1.Type2List);
                            if ((type1.Type2List.Count + type1.Type1Flavor.Count) > 1) {
                                printer += ")";
                            } else if ((type1.Type2List.Count + type1.Type1Flavor.Count) == 1) {
                                printer += "";
                            } else {
                                printer += " ";
                            }
                        } else {
                            printer += PrintTypes(type1.Type1Name, type1.Type1Flavor);
                        }
                    } else {
                        printer += ", " + type1.Type1Name;
                        if (type1.Type2List.Count > 0) {
                            if ((type1.Type2List.Count + type1.Type1Flavor.Count) > 1) {
                                printer += " (";
                            } else {
                                printer += " ";
                            }
                            printer += OrderType1Type2(type1.Type1Flavor, type1.Type2List);
                            if ((type1.Type2List.Count + type1.Type1Flavor.Count) > 1) {
                                printer += ")";
                            } else if ((type1.Type2List.Count + type1.Type1Flavor.Count) == 1) {
                                printer += "";
                            } else {
                                printer += " ";
                            }
                        } else {
                            printer += PrintTypes(type1.Type1Name, type1.Type1Flavor);
                        }
                    }

                }
            }


            return printer;
        }

        private static string OrderType1Type2(List<Flavor> Type1Flavor, List<Type2> Type2Flavors) {
            List<string> flavor = new List<string>();
            string printer = string.Empty;

            foreach (Flavor f in Type1Flavor) {
                flavor.Add(f.flavor);
            }
            foreach (Type2 t2 in Type2Flavors) {
                flavor.Add(t2.Type2Name);
            }

            flavor.Sort();

            for (int i = 0; i < flavor.Count; i++) {
                if (Type1Flavor.Exists(o => o.flavor == flavor[i])) {
                    if (i == 0) {
                        printer += flavor[i];
                    } else {
                        printer += ", " + flavor[i];
                    }
                } else {
                    Type2 t2 = Type2Flavors.Find(o => o.Type2Name == flavor[i]);
                    if (i == 0) {
                        printer += t2.Type2Name;
                        printer += PrintTypes(t2.Type2Name, t2.Type2Flavor);
                    } else {
                        printer += ", " + t2.Type2Name;
                        printer += PrintTypes(t2.Type2Name, t2.Type2Flavor);
                    }
                }
            }

            return printer;
        }

        private static string PrintTypes(string TypeName, List<Flavor> TypeFlavors) {
            string printer = string.Empty;

            if (TypeFlavors.Count > 0) {
                if (TypeFlavors.Count > 1) {
                    printer += " (";
                    for (int i = 0; i < TypeFlavors.Count; i++) {
                        if (i == 0) {
                            printer += TypeFlavors[i].flavor;
                        } else {
                            printer += ", " + TypeFlavors[i].flavor;
                        }
                    }
                    printer += ")";
                } else {
                    printer += " " + TypeFlavors[0].flavor;
                }
            }

            return printer;
        }
        #endregion

    }

    #region Container Classes
    class Flavor {
        public string flavor { get; set; }
        public bool IsCRT { get; set; }
    }

    class Type2 {
        public string Type2Name { get; set; }
        public List<Flavor> Type2Flavor { get; set; }
    }

    class Type1 {
        public string Type1Name { get; set; }
        public List<Type2> Type2List { get; set; }
        public List<Flavor> Type1Flavor { get; set; }
    }

    class Company {
        public string CompanyName { get; set; }
        public List<Type1> Type1List { get; set; }
        public List<Flavor> CompanyFlavor { get; set; }
        public bool IsFAC { get; set; }
    }

    class Category {
        public string CategoryName { get; set; }
        public List<Company> CompanyList { get; set; }
    }
    #endregion
}
//if (comp.CompanyFlavor != null && comp.CompanyFlavor.Count > 0) {
//                            for (int i = 0; i < comp.CompanyFlavor.Count; i++) {
//                                if (i == 0) {
//                                    compflav += comp.CompanyFlavor[i].flavor;
//                                } else {
//                                    compflav += ", " + comp.CompanyFlavor[i].flavor;
//                                }
//                            }
//                        }
//                        if (comp.Type1List != null && comp.Type1List.Count > 0) {
//                            for (int j = 0; j < comp.Type1List.Count; j++) {
//                                if (comp.CompanyFlavor != null && comp.CompanyFlavor.Count > 0) {
//                                    compflav += ", " + comp.Type1List[j].Type1Name;
//                                } else {
//                                    compflav += comp.Type1List[j].Type1Name;
//                                }
//                                if (comp.Type1List[j].Type1Flavor != null && comp.Type1List[j].Type1Flavor.Count > 0) {
//                                    if (comp.Type1List[j].Type1Flavor.Count == 1) {
//                                        compflav += " " + comp.Type1List[j].Type1Flavor[0].flavor;
//                                        if (comp.Type1List[j].Type2List != null && comp.Type1List.Count > 0) {
//                                            for (int l = 0; l < comp.Type1List[j].Type2List.Count; l++) {
//                                                compflav += ", " + comp.Type1List[j].Type2List[l].Type2Name;
//                                                if (comp.Type1List[j].Type2List[l].Type2Flavor.Count == 1) {
//                                                    compflav += " " + comp.Type1List[j].Type2List[l].Type2Flavor[0].flavor;
//                                                } else {
//                                                    compflav += " (";
//                                                    for (int m = 0; m < comp.Type1List[j].Type2List[l].Type2Flavor.Count; m++) {
//                                                        if (m == 0) {
//                                                            compflav += comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                        } else {
//                                                            compflav += "," + comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                        }
//                                                    }
//                                                    compflav += ") ";
//                                                }
//                                            }
//                                        }
//                                    } else {
//                                        compflav += " (";
//                                        for (int k = 0; k < comp.Type1List[j].Type1Flavor.Count; k++) {
//                                            if (k == 0) {
//                                                compflav += comp.Type1List[j].Type1Flavor[k].flavor;
//                                            } else {
//                                                compflav += ", " + comp.Type1List[j].Type1Flavor[k].flavor;
//                                            }
//                                        }
//                                        if (comp.Type1List[j].Type2List != null && comp.Type1List.Count > 0) {
//                                            for (int l = 0; l < comp.Type1List[j].Type2List.Count; l++) {
//                                                compflav += ", " + comp.Type1List[j].Type2List[l].Type2Name;
//                                                if (comp.Type1List[j].Type2List[l].Type2Flavor.Count == 1) {
//                                                    compflav += " " + comp.Type1List[j].Type2List[l].Type2Flavor[0].flavor;
//                                                } else {
//                                                    compflav += " (";
//                                                    for (int m = 0; m < comp.Type1List[j].Type2List[l].Type2Flavor.Count; m++) {
//                                                        if (m == 0) {
//                                                            compflav += comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                        } else {
//                                                            compflav += ", " + comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                        }
//                                                    }
//                                                    compflav += ") ";
//                                                }
//                                            }
//                                        }
//                                        compflav += ") ";
//                                    }
//                                } else {
//                                    if (comp.Type1List[j].Type2List != null && comp.Type1List.Count > 0) {
//                                        for (int l = 0; l < comp.Type1List[j].Type2List.Count; l++) {
//                                            compflav += " " + comp.Type1List[j].Type2List[l].Type2Name;
//                                            if (comp.Type1List[j].Type2List[l].Type2Flavor.Count == 1) {
//                                                compflav += " " + comp.Type1List[j].Type2List[l].Type2Flavor[0].flavor;
//                                            } else {
//                                                compflav += " (";
//                                                for (int m = 0; m < comp.Type1List[j].Type2List[l].Type2Flavor.Count; m++) {
//                                                    if (m == 0) {
//                                                        compflav += comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                    } else {
//                                                        compflav += ", " + comp.Type1List[j].Type2List[l].Type2Flavor[m].flavor;
//                                                    }
//                                                }
//                                                compflav += ") ";
//                                            }
//                                        }
//                                    }
//                                }
//                            }
//                        }

//private static void SortProducts(List<Category> Products) {
//            Products.Sort(delegate(Category p1, Category p2) {
//                return p1.CategoryName.CompareTo(p2.CategoryName);
//            });

//            foreach (Category c in Products) {
//                c.CompanyList.Sort(delegate(Company c1, Company c2) {
//                    return c1.CompanyName.CompareTo(c2.CompanyName);
//                });
//                foreach (Company comp in c.CompanyList) {
//                    comp.Type1List.Sort(delegate(Type1 t1, Type1 t2) {
//                        return t1.Type1Name.CompareTo(t2.Type1Name);
//                    });
//                    foreach (Type1 t1 in comp.Type1List) {
//                        t1.Type2List.Sort(delegate(Type2 ty21, Type2 ty22) {
//                            return ty21.Type2Name.CompareTo(ty22.Type2Name);
//                        });
//                    }
//                }
//            }
//        }