using Triangulation;

Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
var logic = new DomainLogic();
Application.Run(new AppForm(logic));
