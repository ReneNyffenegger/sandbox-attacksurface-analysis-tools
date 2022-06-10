
public class tq84 {

   private static int indent_ = 0;

   public static void indent(string txt,
     [System.Runtime.CompilerServices.CallerMemberName        ] string mbr = "",
     [System.Runtime.CompilerServices.CallerFilePath          ] string fil = "",
     [System.Runtime.CompilerServices.CallerLineNumber        ] int    lin =  0
//   [System.Runtime.CompilerServices.CallerArgumentExpression] string arg = "") // Implemented in compiler for C# 10 and later
   ) {
        indent_ ++;
        print(txt, mbr, fil, lin);
   }

   public static void dedent() { indent_ -- ;}
   public static void print(string txt,
     [System.Runtime.CompilerServices.CallerMemberName        ] string mbr = "",
     [System.Runtime.CompilerServices.CallerFilePath          ] string fil = "",
     [System.Runtime.CompilerServices.CallerLineNumber        ] int    lin =  0
//   [System.Runtime.CompilerServices.CallerArgumentExpression] string arg = "") // Implemented in compiler for C# 10 and later

   ) {

      System.Console.WriteLine(System.String.Empty.PadLeft(indent_*2) + txt + " (" + fil + "@" + lin + ", " + mbr + ")");
   }

}
