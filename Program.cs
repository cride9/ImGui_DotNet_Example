using ImGuiNET;
using ClickableTransparentOverlay;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace ImGuiC_ {
    public class Program : Overlay {
        static ImColor childColor = new( ) { Value = new Vector4( 0.15f, 0.15f, 0.15f, 1f ) };
        static ImColor backgroundColor = new( ) { Value = new Vector4( 0.1f, 0.1f, 0.1f, 1f ) };

        bool checkbox1 = false;
        Vector3 coloredit1 = new();
        Vector3 coloredit2 = new();
        protected override void Render( ) {

            PushStyles( );
            ImGui.Begin( "Window", ImGuiWindowFlags.NoTitleBar );
            {
                ImGui.BeginChild( "##id1", new Vector2( ImGui.GetContentRegionAvail().X / 2, ImGui.GetContentRegionAvail().Y ), ImGuiChildFlags.Border );
                {
                    ImGui.Checkbox( "checkbox", ref checkbox1 );
                    ImGui.ColorEdit3( "color editor##1", ref coloredit1, ImGuiColorEditFlags.NoBorder | ImGuiColorEditFlags.NoInputs | ImGuiColorEditFlags.NoSidePreview );
                    ImGui.ColorEdit3( "color editor##2", ref coloredit2 );
                }
                ImGui.EndChild( );
            }
            ImGui.End( );

            ImGui.PopStyleColor( );
            PopStyles( );
        }

        void PushStyles() {

            // colors
            ImGui.PushStyleColor( ImGuiCol.ChildBg, childColor.Value );
            ImGui.PushStyleColor( ImGuiCol.WindowBg, backgroundColor.Value );

            // styles
            ImGui.PushStyleVar( ImGuiStyleVar.WindowMinSize, new Vector2( 500, 300 ) );
            ImGui.PushStyleVar( ImGuiStyleVar.WindowRounding, 10f );
            ImGui.PushStyleVar( ImGuiStyleVar.WindowPadding, new Vector2( 10f, 10f ) );
            ImGui.PushStyleVar( ImGuiStyleVar.CellPadding, new Vector2( 25f, 25f ) );
            ImGui.PushStyleVar( ImGuiStyleVar.ChildBorderSize, 1f );
            ImGui.PushStyleVar( ImGuiStyleVar.ChildRounding, 10f );
        }

        void PopStyles( ) {

            ImGui.PopStyleColor( 2 );
            ImGui.PopStyleVar( 6 );
        }

        public static void Main( string[] args ) {

            Console.WriteLine( "Starting..." );

            Program _this = new( );
            _this.Start( ).Wait( );
            _this.Size = new System.Drawing.Size( 1920, 1080 );
        }
    }
}
