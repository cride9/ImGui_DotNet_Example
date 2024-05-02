using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ImGuiC_ {
    public class Program : Overlay {
        static ImColor childColor = new( ) { Value = new Vector4( 0.15f, 0.15f, 0.15f, 1f ) };
        static ImColor backgroundColor = new( ) { Value = new Vector4( 0.1f, 0.1f, 0.1f, 1f ) };
        static ImColor borderColor = new( ) { Value = new Vector4( 0.3f, 0.3f, 0.3f, 1f ) };

        bool checkbox1 = false;

        protected override void Render( ) {

            PushStyles( );
            ImGui.Begin( "Window" );
            {
                ImGui.BeginChild( "##id1", new Vector2( ImGui.GetContentRegionAvail().X / 2, ImGui.GetContentRegionAvail().Y ), ImGuiChildFlags.Border );
                {
                    ImGui.Checkbox( "checkbox##1", ref checkbox1 );
                    ImGui.ColorEdit4( "Border##1", ref borderColor.Value, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoSidePreview );
                    ImGui.SameLine( );
                    ImGui.ColorEdit4( "Window background##2", ref backgroundColor.Value, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoSidePreview );
                    ImGui.ColorEdit4( "Child background##2", ref childColor.Value, ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoSidePreview );
                }
                ImGui.EndChild( );
                ImGui.SameLine( );
                ImGui.BeginChild( "##id2", new Vector2( ImGui.GetContentRegionAvail( ).X, ImGui.GetContentRegionAvail( ).Y ), ImGuiChildFlags.Border );
                {
                    ImGui.Checkbox( "checkbox##2", ref checkbox1 );
                    ImGui.BeginListBox( "#listbox" );
                    {
                        for ( int i = 0; i < (int)ImGuiStyleVar.COUNT; i++ ) {
                            ImGui.Selectable( ( ImGuiStyleVar )i, false);

                        }
                    }
                    ImGui.EndListBox( );
                }
                ImGui.EndChild( );
            }
            ImGui.End( );

            ImGui.Begin( "Window##2" );
            {
                if ( ImGui.Button( "Heloka nyomj meg" ) )
                    Console.WriteLine( "Megnyomtad!" );
            }
            ImGui.End( );

            ImGui.PopStyleColor( );
            PopStyles( );
        }

        void PushStyles() {

            // colors
            ImGui.PushStyleColor( ImGuiCol.ChildBg, childColor.Value );
            ImGui.PushStyleColor( ImGuiCol.WindowBg, backgroundColor.Value );
            ImGui.PushStyleColor( ImGuiCol.Border, borderColor.Value );

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

            ImGui.PopStyleColor( 2 );
            ImGui.PopStyleVar( 7 );
        }

        public static void Main( string[] args ) {

            Console.WriteLine( "Starting..." );

            Program _this = new( );
            _this.Start( ).Wait( );
            _this.Size = new System.Drawing.Size( 1920, 1080 );
        }
    }
}
