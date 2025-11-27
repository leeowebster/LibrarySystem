using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibrarySystem.Domain.Models
{
    public class ExternalApiResponseDTO
    {
        
        public string Title { get; set; }

        public List<string> Series { get; set; }

        // 3. Lidar com o nome snake_case do JSON (usando JsonPropertyName)
        [JsonPropertyName("isbn_13")]
        public List<string> Isbn13 { get; set; }

        // 4. Mapeamento para o objeto aninhado "created"
        public CreatedDate Created { get; set; }

        // 5. Propriedades que não existem no JSON:
        // O JSON fornecido NÃO TEM 'Author' e 'YearPublished' nos campos raiz.
        // Você não pode mapeá-los diretamente; você precisará de uma busca secundária ou de lógica.

        // Para fins de compilação, mantemos suas propriedades:
        public string Author { get; set; } // OBS: Este campo virá nulo ou deve ser preenchido por outra chamada.
        public string YearPublished { get; set; } // OBS: Este campo virá nulo ou deve ser preenchido por outra chamada.
    }

    // Classe aninhada para o campo "created"
    public class CreatedDate
    {
        // Mapeia o valor real da data dentro do objeto "created"
        [JsonPropertyName("value")]
        public DateTime Value { get; set; }
    }
}
