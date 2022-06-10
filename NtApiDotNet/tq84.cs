
public class tq84 {

   private static int indent_ = 0;

   public static void indent(string txt) { indent_ ++ ; print(txt);}
   public static void dedent() { indent_ -- ;}
   public static void print(string txt) {
      System.Console.WriteLine(System.String.Empty.PadLeft(indent_*2) + txt);
   }

}
