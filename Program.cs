using System.Runtime.CompilerServices;
using ClickableTransparentOverlay;
using Newtonsoft.Json;
using System.Numerics;
using System.Drawing;
using ImGuiNET;

namespace ImGuiC_ {
    public class Program : Overlay {

        Vector4 temp = new( );
        int currentStyleColor = 0;
        static List<StringVectorPair> ImColStrings;

        protected unsafe override void Render( ) {

            // this shows a full detailed and modifyable imgui demo window
            // NOTE: Would be good to do an universal class to save the demo window "ImGuiStyleVars" for easier development
            //ImGui.ShowDemoWindow( );

            PushStyles( );
            ImGui.Begin( "Window##1", ImGuiWindowFlags.NoTitleBar );
            {
                // draw listbox with the color variables
                ImGui.ListBox( "##listbox", ref currentStyleColor, ImColStrings.Select( select => select.Name ).ToArray( ), ImColStrings.Count );

                // get the current color variable based on the selected color
                // NOTE: This functions returns a pointer to the color so we have to dereference it
                temp = *ImGui.GetStyleColorVec4( ( ImGuiCol )currentStyleColor );

                // draw the color picker with a temp color
                // NOTE: temp color is needed becuase of the CS1510 error (A ref or out value must be an assignable variable)
                ImGui.ColorEdit4( "##color", ref temp, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoSidePreview | ImGuiColorEditFlags.AlphaBar );

                // Set the current selected color
                ImColStrings[ ( int )currentStyleColor ] = new( Enum.Parse<ImGuiCol>( currentStyleColor.ToString( ) ).ToString( ), temp);

                // Make the json saving here :D
                if ( ImGui.Button( "Save color config", new Vector2( ImGui.GetContentRegionAvail( ).X, 30 ) ) )
                    SerializeToJson( ImColStrings );
            }
            ImGui.End( );
            PopStyles( );
        }

        void PushStyles() {

            // Push colors that are set
            foreach ( var item in ImColStrings )
                if ( item.Vector is not null )
                    ImGui.PushStyleColor( Enum.Parse<ImGuiCol>( item.Name ), (Vector4)item.Vector );

            // styles
            ImGui.PushStyleVar( ImGuiStyleVar.WindowMinSize, new Vector2( 500, 300 ) );
            ImGui.PushStyleVar( ImGuiStyleVar.WindowRounding, 10f );
            ImGui.PushStyleVar( ImGuiStyleVar.WindowPadding, new Vector2( 10f, 10f ) );
            ImGui.PushStyleVar( ImGuiStyleVar.CellPadding, new Vector2( 25f, 25f ) );
            ImGui.PushStyleVar( ImGuiStyleVar.ChildBorderSize, 1f );
            ImGui.PushStyleVar( ImGuiStyleVar.ChildRounding, 10f );
            ImGui.PushStyleVar( ImGuiStyleVar.FrameRounding, 5f );
        }

        void PopStyles( ) {

            // Pop all the styles
            ImGui.PopStyleColor( ImColStrings.Count( where => where.Vector is not null ) );
            ImGui.PopStyleVar( 7 );
        }

        public static void Main( string[] args ) {

            // create if cfg not exists
            if ( !File.Exists( @"colorsSave.json" ) )
                File.Create( @"colorsSave.json" ).Close();

            // load from json if it has any data
            ImColStrings = DeserializeFromJson( );

            // bebebe wrong data check
            if ( ImColStrings is null ) {
                ImColStrings = new( );
                for ( ImGuiCol i = ImGuiCol.Text; i < ImGuiCol.COUNT; i++ )
                    ImColStrings.Add( new( i.ToString( ), null ) );
            }

            // create imgui window :D
            Program _this = new( );
            _this.Start( ).Wait( );
            _this.Size = new Size( 1920, 1080 );
        }

        // saving to json
        public static void SerializeToJson( List<StringVectorPair> list ) =>
            File.WriteAllText( @"colorsSave.json", JsonConvert.SerializeObject( list, Formatting.Indented ) );
        
        // load from json
        public static List<StringVectorPair>? DeserializeFromJson( ) =>
            JsonConvert.DeserializeObject<List<StringVectorPair>>( File.ReadAllText( @"colorsSave.json" ) );
        
    }

    // json save file
    public struct StringVectorPair {
        public string Name { get; set; }
        public Vector4? Vector { get; set; }

        public StringVectorPair( string _name, Vector4? _vector ) {
            Name = _name;
            Vector = _vector;
        }
    }
}
