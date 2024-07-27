using ArganaWeedClietApi;
using ArganaWeedClietApi.Models;

namespace ArganaWeedApp.DTOs
{
    public class EmplacementsResponse:BaseResponse
    {
        public List<Emplacement> emplacements { get; set; }
    }
}
