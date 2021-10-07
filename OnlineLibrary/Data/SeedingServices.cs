using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OnlineLibrary.Models;
using OnlineLibrary.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineLibrary.Data
{
    public class SeedingServices
    {
        private readonly AppDbContext _context;

        public SeedingServices(AppDbContext context)
        {
            _context = context;
        }

        public static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Default", "Author" };

            foreach (string role in roles)
            {
                bool roleExists = await roleManager.RoleExistsAsync(role);
                if (roleExists is false)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public void SeedDb()
        {
            if (_context.ApplicationUsers.Any() || _context.Authors.Any() || _context.Books.Any())
                return;

            Author[] authors = {
                new("Eva Heller", new DateTime(1948, 4, 8), "Eva Heller foi uma escritora e cientista social alemã."),
                new("Will Gompertz", new DateTime(1965, 8, 25), "William Edward Gompertz foi o editor de artes da BBC antes de assumir o cargo de Diretor de Artes e Aprendizagem do Barbican Centre em 1º de junho de 2021. Gompertz frequentou a Dulwich Preparatory School, em Cranbrook, Kent."),
                new("Barack Obama", new DateTime(1961, 8, 4), "Barack Hussein Obama II é um advogado e político norte-americano que serviu como o 44.º presidente dos Estados Unidos de 2009 a 2017, sendo o primeiro afro-americano a ocupar o cargo."),
                new("Thiago Nigro", new DateTime(1990, 10, 7), "Thiago Nigro é idealizador e sócio proprietário do projeto O Primo Rico, veículo de internet voltado para ensinar seu público sobre educação financeira e gestão de dinheiro."),
                new("T. Harv Eker", new DateTime(1954, 6, 10), "T. Harv Eker é um autor, empresário e palestrante motivacional conhecido por suas teorias sobre riqueza e motivação. Ele é o autor do livro Secrets of the Millionaire Mind publicado pela HarperCollins."),
                new("Eric Evans", new DateTime(1950, 2, 27), "Eric Evans is a thought leader in software design and domain modeling."),
                new("Robert Cecil Martin", new DateTime(1951, 1, 1), "Robert Cecil Martin, também conhecido como 'Uncle Bob', é uma grande personalidade da comunidade de desenvolvimento de software, métodos ágeis e software craftsmanship, atuando na área desde 1970. Atualmente é consultor internacional e autor de vários livros abordando o tema."),
                new("Maurício de Sousa", new DateTime(1935, 10, 27), "Mauricio Araújo de Sousa é um cartunista, empresário e escritor brasileiro. É um dos mais famosos cartunistas do Brasil, criador da Turma da Mônica e membro da Academia Paulista de Letras."),
                new("Thais Godinho", new DateTime(), null),
                new("Mari Ono", new DateTime(), null),
                new("Rita Lobo", new DateTime(), null),
                new("Senac Editoras", new DateTime(), "Conta com um catálogo ativo de mais de 1.000 livros e lança, anualmente, cerca de 80 novos títulos. Por dispor de uma boa rede de distribuidores, coloca seus produtos em praticamente todas as cidades do Brasil."),
                new("Equipe AlfaCon", new DateTime(), "O AlfaCon é o Preparatório Que Mais Aprova no Brasil."),
                new("Walter J. Pfeil", new DateTime(), null),
                new("Jorge Munaiar Neto", new DateTime(), null),
                new("Genival Fernandes de Freitas", new DateTime(), "Possui Graduação em Enfermagem pela EEUSP (1988), Graduação em Direito pela Pontifícia Universidade Católica PUC-São Paulo (2000), Mestrado na Área de Administração em Serviços de Enfermagem pela Universidade de São Paulo (2002) e Doutorado pela Universidade de São Paulo (2005)."),
                new("Yuval Harari", new DateTime(1976, 2, 24), "Yuval Noah Harari é um professor israelense de História e autor do best-seller internacional Sapiens: Uma breve história da humanidade, Homo Deus: Uma Breve História do Amanhã e 21 Lições para o Século 21. Seu último lançamento é Notas sobre a Pandemia: E breves lições para o mundo pós-coronavírus."),
                new("Laurentino Gomes", new DateTime(1956, 2, 17), "José Laurentino Gomes é um jornalista e escritor brasileiro."),
                new("Krishna Mahon", new DateTime(), null),
                new("Jorge de Rezende", new DateTime(), null),
                new("Eric Kandel", new DateTime(1929, 11, 7), "Eric Richard Kandel é um neurocientista austríaco, naturalizado estadunidense. Foi agraciado, juntamente com o sueco Arvid Carlsson e com o estadunidense Paul Greengard, com o Nobel de Fisiologia ou Medicina de 2000, por descobertas envolvendo a transmissão de sinais entre células nervosas no cérebro humano."),
                new("C.J. Tudor", new DateTime(1972, 1, 1), "C.J.Tudor é um autor britânico cujos livros incluem The Chalk Man e The Hiding Place.Ela nasceu em Salisbury, Inglaterra, mas cresceu em Nottingham, onde ainda vive."),
                new("Karen M. McManus", new DateTime(), "Karen M. McManus é uma autora americana de ficção para jovens adultos. Ela é mais conhecida por seu primeiro romance, One of Us Is Lying, que passou mais de 160 semanas na lista de bestsellers do New York Times. Ele recebeu uma revisão com estrela da Publishers Weekly."),
                new("Gabriela Prioli", new DateTime(1986, 1, 21), "Gabriela Prioli Della Vedova é uma advogada criminalista, professora universitária, comentarista política e apresentadora de TV brasileira que ficou conhecida nacionalmente por ter integrado o quadro '  O Grande Debate'', do telejornal CNN Novo Dia, da rede CNN Brasil, atuando como comentarista política."),
                new("C. S. Lewis", new DateTime(1898, 11, 29), "Clive Staples Lewis, comumente referido como C. S. Lewis, foi um professor universitário, escritor, romancista, poeta, crítico literário, ensaísta e teólogo irlandês. Durante sua carreira acadêmica, foi professor e membro do Magdalen College, tanto da Universidade de Oxford como da Universidade de Cambridge."),
                new("Rodrigo Bibo", new DateTime(), null),
                new("Colleen Hoover", new DateTime(1979, 12, 11), "Colleen Hoover é a autora mais vendida do New York Times com onze romances e cinco novelas. Seus romances se enquadram nas categorias Novo Adulto e Jovens Adultos. Hoover publicou seu primeiro romance, Slammed, em janeiro de 2012."),
                new("Beth O’Leary", new DateTime(1929, 1, 1), null),
                new("Peter William Atkins", new DateTime(1940, 8, 10), "Peter William Atkins é um químico britânico e professor do Lincoln College da Universidade de Oxford. Ele é um produtivo escritor de livros didáticos populares de química, incluindo Physical Chemistry, Inorganic Chemistry, e Molecular Quantum Mechanics."),
                new("Ailton Krenak", new DateTime(1953, 9, 29), "Ailton Alves Lacerda Krenak, mais conhecido como Ailton Krenak, é um líder indígena, ambientalista, filósofo, poeta e escritor brasileiro da etnia indígena crenaque."),
                new("Frank Herbert", new DateTime(1920, 10, 8), "Frank Patrick Herbert foi um escritor de ficção científica e jornalista americano de grande sucesso comercial e de crítica. Ele é mais conhecido pela obra Duna, e os cinco livros subseqüentes da série."),
                new("Júlio Verne", new DateTime(1928, 2, 8), "Jules Gabriel Verne, conhecido nos países de língua portuguesa por Júlio Verne, foi um escritor francês. Júlio Verne foi o primogênito dos cinco filhos de Pierre Verne, advogado, e Sophie Allote de la Fuÿe, esta de uma família burguesa de Nantes."),
                new("Lori Gottlieb", new DateTime(1966, 12, 20), "Lori Gottlieb é uma escritora e psicoterapeuta americana. Ela é autora do best-seller do New York Times, Maybe You Should Talk to Someone, que está sendo adaptado como uma série de TV."),
                new("Dale Carnegie", new DateTime(1888, 10, 24), "Dale Carnegie foi um formador, escritor e orador norte-americano. Fundou o que é hoje uma rede mundial de mais de 3.000 instrutores e escritórios em aproximadamente 97 países que já formou mais de 9 milhões de pessoas no mundo."),
                new("Bernadinho", new DateTime(), null),
                new("Christopher Clarey", new DateTime(), "Christopher Clarey é um jornalista americano, colunista mundial de esportes e escritor de tênis do The New York Times. Ele também é colaborador regular da ESPN."),
                new("Jon Krakauer", new DateTime(1954, 4, 12), "Jon Krakauer é um escritor e montanhista americano. Ele é o autor de best-sellers de livros de não ficção - Into the Wild; Into Thin Air; Sob a Bandeira do Céu; e Where Men Win Glory: The Odyssey of Pat Tillman - bem como vários artigos de revistas."),
            };

            Book[] books = {
                new("A psicologia das cores", Genre.Arts, "Este livro aborda a relação das cores com os nossos sentimentos e mostra como as duas coisas não se combinam por acaso, já que as relações entre ambas não são apenas questões de gosto, mas sim experiências universais profundamente enraizadas na nossa linguagem e no nosso pensamento.", 113, authors[0]),
                new("Isso é arte?", Genre.Arts, "Original, irreverente, acessível e tecnicamente impecável, Isso é arte? conduz o leitor por uma excitante viagem através de mais de 150 anos de arte moderna, do impressionismo até os dias de hoje.", 65, authors[1]),
                new("Barack Obama: Uma terra prometida", Genre.Biographies, "Um relato íntimo e fascinante da história em formação ― feito pelo líder que nos inspirou a acreditar no poder da democracia.", 54, authors[2]),
                new("Do Mil ao Milhão. Sem Cortar o Cafezinho", Genre.Bussiness, null, 15.99, authors[3]),
                new("Os segredos da mente milionária", Genre.Bussiness, null, 18.99, authors[4]),
                new("Domain-Driven Design", Genre.ComputersAndTechnology, "O design orientado a domínio é o conceito de que a estrutura e a linguagem do código do software devem corresponder ao domínio do negócio. Por exemplo, se um software processa solicitações de empréstimo, ele pode ter classes como LoanApplication e Customer e métodos como AcceptOffer e Withdraw.", 459, authors[5]),
                new("Código limpo: Habilidades práticas do Agile Software", Genre.ComputersAndTechnology, "Robert C. Martin, apresenta um paradigma revolucionário com Código limpo: Habilidades Práticas do Agile Software. Martin se reuniu com seus colegas do Mentor Object para destilar suas melhores e mais ágeis práticas de limpar códigos “dinamicamente” em um livro que introduzirá gradualmente dentro de você os valores da habilidade de um profissional de softwares e lhe tornar um programador melhor.", 62, authors[6]),
                new("Turma da Mônica - Clássicos Ilustrados - O Pequeno Polegar", Genre.ComicsAndGraphicNovels, "Cada livro desta coleção traz um conto clássico famoso no mundo todo, representado pelas personagens da Turma da Mônica.", 9.90, authors[7]),
                new("Cebolinha - Dia das Bruxas", Genre.ComicsAndGraphicNovels, null, 41, authors[7]),
                new("Vida organizada: Como definir prioridades e transformar seus sonhos em objetivos", Genre.CraftsHobbiesAndHome, null, 26.90, authors[8]),
                new("Origami para crianças", Genre.CraftsHobbiesAndHome, "O origami é uma arte milenar e muito conhecida. Divirta-se com este livro e aprenda a fazer muitos desenhos de origami. Use a sua criatividade para finalizar as artes.", 9.90, authors[9]),
                new("Panelinha: receitas que funcionam", Genre.Culinary, "Panelinha é o site que Rita Lobo criou no ano de 2000 para ensinar a preparar pratos saudáveis, revelando truques e manhas, de modo que qualquer pessoa consiga fazer. Para o livro, foram reunidas sugestões para variadas situações e ocasiões do cotidiano.", 83, authors[10]),
                new("Chef profissional", Genre.Culinary, "Chef profissional é essencial para qualquer chef que deseje aprender e aperfeiçoar suas técnicas. Essa bíblia dos chefs reflete, como poucas obras, a forma como as pessoas cozinham e comem, e ainda procura discutir uma vasta gama de assuntos, que vão de uma análise do trabalho do chef a capítulos específicos sobre diversos refeições.", 211, authors[11]),
                new("Questões de bolso - Polícia rodoviária federal", Genre.Education, "Este material, essencial para preparação, traz 1.360 questões gabaritadas das disciplinas básicas e específicas pertinentes ao concurso.O livro de bolso foi formulado para que o concurseiro possa utilizá-lo com facilidade, podendo transportá-lo durante suas atividades cotidianas, uma vez que possui um tamanho reduzido.", 19.99, authors[12]),
                new("ENEM - Exame nacional do ensino médio - Questões comentadas", Genre.Education, "A Editora AlfaCon reuniu profissionais especialistas comgrande experiência em ENEM e trouxe para você esta obra que inclui questões dos últimos 7 anos de prova, incluindo as do ENEM 2018. São mais de 1250 questões resolvidas e comentadas. As novidades não param por aí! O ENEM Questões 2019 AlfaCon também trás: Informações gerais sobre o exame.", 42.49, authors[12]),
                new("Estruturas de Madeira", Genre.EngineeringAndTransportation, "Em Estruturas de Madeira, os autores apresentam os produtos de madeira e seu emprego em estruturas civis, as propriedades físicas e mecânicas da madeira e a metodologia de dimensionamento dos elementos estruturais no contexto do assim chamado método dos estados limites.", 137.99, authors[13]),
                new("Segurança nas Estruturas", Genre.EngineeringAndTransportation, "O livro apresenta, discute e demonstra, por meio de exemplos, a evolução das teorias desenvolvidas para a introdução de segurança nas estruturas, entendendo-se segurança como a capacidade da estrutura de suportar as forças que estará submetida durante a sua vida útil.", 215, authors[14]),
                new("Enfermagem Forense", Genre.Health, "A Enfermagem Forense é uma área de saber e de práticas mais desenvolvida em países como Portugal, Estados Unidos e Japão. Costuma chamar a atenção dos mais variados tipos de profissionais pela sua semelhança com os seriados de ficção, que usam a perícia e a investigação para solucionar crimes, na maioria das vezes, de alta complexidade.", 148, authors[15]),
                new("Sapiens: Uma breve história da humanidade", Genre.History, "Na nova edição do livro que conquistou milhões de leitores ao redor do mundo, Yuval Noah Harari questiona tudo o que sabemos sobre a trajetória humana no planeta ao explorar quem somos, como chegamos até aqui e por quais caminhos ainda poderemos seguir.", 32, authors[16]),
                new("Escravidão: Da corrida do ouro em Minas Gerais até a chegada da corte de dom João ao Brasil", Genre.History, null, 34, authors[17]),
                new("Os 10 (ou mais) mandamentos da solteira", Genre.HumorAndEntertainment, "Como é ser jovem e solteira nos dias de hoje? Um guia de mesa de bar... ou talvez um não guia, em que os conselhos não precisam ser levados ao pé da letra, os mandamentos são flexíveis e as histórias podem ou não ser verídicas.", 22, authors[18]),
                new("Rezende Obstetrícia", Genre.Medical, "Clássico que tem contribuído para a formação de várias gerações de estudantes e profissionais da saúde.", 407, authors[19]),
                new("Princípios de Neurociências", Genre.Medical, "Decifrar a relação entre o encéfalo e o comportamento humano sempre foi um dos maiores desafios da ciência. Escrito por importantes pesquisadores da área, incluindo Eric R. Kandel, vencedor do Prêmio Nobel em 2000, este clássico absoluto apresenta uma visão atualizada da disciplina de neurociências, refletindo a pesquisa mais recente que transformou o conhecimento na última década.", 389, authors[20]),
                new("O Que Aconteceu Com Annie", Genre.MysteryThrillerAndSuspense, "Quando Joe Thorne era adolescente, sua irmã mais nova desapareceu. Vinte e cinco anos depois, um e-mail anônimo o leva mais uma vez ao passado: “Eu sei o que aconteceu com sua irmã. Está acontecendo de novo.”", 29.99, authors[21]),
                new("Um de nós está mentindo", Genre.MysteryThrillerAndSuspense, "Cinco alunos entram em detenção na escola e apenas quatro saem com vida. Todos são suspeitos e cada um tem algo a esconder. Este é o enredo do Um de nós está mentindo, romance de estreia de Karen M. McManus.", 29, authors[22]),
                new("Política é para todos", Genre.PoliticsAndSocialScience, null, 26.90, authors[23]),
                new("Cristianismo puro e simples", Genre.Religion, "Lewis apresenta os principais elementos da cosmovisão cristã, gradativamente conduzindo o leitor a temas mais profundos e complexos, provocando reflexão e debate. Nesta edição especial e com tradução de uma das maiores especialistas em Lewis do Brasil, você vai encontrar as palavras que encorajaram e fortaleceram milhares de ouvintes em tempos de guerra - e ainda reverberam mais de 70 anos depois.", 18, authors[24]),
                new("O Deus que destrói sonhos", Genre.Religion, "Uma tentação constante que cerca a vida cristã é a inversão do chamado: a presunção de que Deus precisa abençoar o meu caminho e me seguir em meus planos e sonhos. Essa postura é enganosa e faz parecer que Deus só é fiel quando me abençoa.", 22.90, authors[25]),
                new("É Assim que Acaba", Genre.Romance, "O romance mais pessoal da carreira de Colleen Hoover, É assim que acaba discute temas como violência doméstica e abuso psicológico de forma sensível e direta.", 32, authors[26]),
                new("Teto Para Dois", Genre.Romance, null, 37, authors[27]),
                new("Princípios de Química: Questionando a Vida Moderna e o Meio Ambiente", Genre.Science, "Princípios de Química: questionando a vida moderna e o meio ambiente, sétima edição, apresenta todos os fundamentos da química de forma clara e precisa, utilizando inúmeras ferramentas pedagógicas. O conteúdo está organizado em 85 tópicos curtos, distribuídos em 11 grupos temáticos.", 239.99, authors[28]),
                new("Ideias para adiar o fim do mundo", Genre.Science, "Uma parábola sobre os tempos atuais, por um de nossos maiores pensadores indígenas.", 15.49, authors[29]),
                new("Duna: 1", Genre.SciFi, "Duna é um triunfo da imaginação, que influenciará a literatura para sempre. Esta edição inédita, com introdução de Neil Gaiman, apresenta ao leitor o universo fantástico criado por Herbert e que será adaptado ao cinema por Denis Villeneuve, diretor de A chegada e de Blade Runner 2049.", 29.99, authors[30]),
                new("Viagem ao centro da Terra", Genre.SciFi, "O professor Lidenbrock consegue decifrar um enigma do pergaminho de um cientista do século XII e se junta ao seu sobrinho, o jovem Áxel, para checar a possibilidade de chegar ao centro da Terra seguindo o relato decifrado.", 9.95, authors[31]),
                new("Talvez você deva conversar com alguém: Uma terapeuta, o terapeuta dela e a vida de todos nós", Genre.SelfHelp, "De modo geral, buscamos a ajuda de um terapeuta para melhor compreender as angústias, os medos, a culpa ou quaisquer outros sentimentos que nos causam desconforto e sofrimento. Mas quantos de nós já paramos para perguntar: o terapeuta está imune à gama de questões que ele auxilia seus pacientes a dirimir e superar, dia após dia? A autora e terapeuta Lori Gottlieb nos mostra essa resposta", 29.90, authors[32]),
                new("Como Fazer Amigos e Influenciar Pessoas", Genre.SelfHelp, "O guia clássico e definitivo para relacionar-se com as pessoas. Como fazer amigos e influenciar pessoas segue sendo um livro inovador, e uma das principais referências do mundo sobre relacionamentos.", 45, authors[33]),
                new("Transformando suor em ouro", Genre.Sports, "Obstinado, persistente, perfeccionista e motivador, Bernardinho se tornou o maior técnico de vôlei da história do Brasil – e um dos grandes treinadores do esporte coletivo em todo o mundo. Transformando suor em ouro é a história de Bernardinho contada por ele mesmo, desde os tempos de jogador até a consagração como técnico com o ouro olímpico.", 23, authors[34]),
                new("Federer: O Homem Que Mudou o Esporte", Genre.Sports, null, 52, authors[35]),
                new("Na natureza selvagem", Genre.Travel, "Narrativa verídica sobre sonhos de juventude que se transformam em pesadelo. O corpo em decomposição de um jovem é encontrado no Alasca. A polícia descobre que se trata de um rapaz de família rica do Leste americano que largou tudo, se internou sozinho na aridez gelada e morreu de inanição. Quem era o garoto? Por que foi para o Alasca? Por que morreu?", 35, authors[36])
            };

            foreach (Author author in authors)
                author.ImagePath = "~/Images/ProfilePhotos/Default.png";
            foreach (Book book in books)
                book.ImagePath = "~/Images/BookImages/Default.png";

            _context.AddRange(authors);
            _context.AddRange(books);
            _context.SaveChanges();
        }
    }
}
