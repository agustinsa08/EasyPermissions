using EasyPermissions.Backend.Helpers;
using EasyPermissions.Backend.UnitsOfWork.Interfaces;
using EasyPermissions.Shared.Entities;
using EasyPermissions.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace EasyPermissions.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;
        private readonly IFileStorage _fileStorage;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork, IFileStorage fileStorage)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
            _fileStorage = fileStorage;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            //await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckAreasAsync();
            await CheckTypeNoticesAsync();
            await CheckNoticesAsync();
            await CheckTypePermissionsAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Talento", "Humano", "talento.humano@yopmail.com", "322 311 4476", "Calle Luna Calle Sol 26", UserType.Admin); 

            await CheckUserAsync("1012", "María", "García", "lider1@yopmail.com", "322 311 4602", "Calle Luna Calle Sol 2", UserType.Leader);
            await CheckUserAsync("1013", "Juan", "Pérez", "lider2@yopmail.com", "322 311 4603", "Calle Luna Calle Sol 3", UserType.Leader);
            await CheckUserAsync("1014", "Ana", "Martínez", "lider3@yopmail.com", "322 311 4604", "Calle Luna Calle Sol 4", UserType.Leader);
            await CheckUserAsync("1015", "Luis", "Hernández", "lider4@yopmail.com", "322 311 4605", "Calle Luna Calle Sol 5", UserType.Leader);
            await CheckUserAsync("1016", "Laura", "Jiménez", "lider5@yopmail.com", "322 311 4606", "Calle Luna Calle Sol 6", UserType.Leader);
            await CheckUserAsync("1017", "Pedro", "Vásquez", "lider6@yopmail.com", "322 311 4607", "Calle Luna Calle Sol 7", UserType.Leader);
            await CheckUserAsync("1018", "Sofía", "Castro", "lider7@yopmail.com", "322 311 4608", "Calle Luna Calle Sol 8", UserType.Leader);
            await CheckUserAsync("1019", "Jorge", "Torres", "lider8@yopmail.com", "322 311 4609", "Calle Luna Calle Sol 9", UserType.Leader);
            await CheckUserAsync("1020", "Isabel", "Mendoza", "lider9@yopmail.com", "322 311 4610", "Calle Luna Calle Sol 10", UserType.Leader);
            await CheckUserAsync("1021", "Carlos", "López", "lider10@yopmail.com", "322 311 4601", "Calle Luna Calle Sol 1", UserType.Leader);

            await CheckUserAsync("1022", "Patricia", "Salazar", "patricia@yopmail.com", "322 311 4612", "Calle Luna Calle Sol 12", UserType.User);
            await CheckUserAsync("1023", "Andrés", "Acuña", "andres@yopmail.com", "322 311 4613", "Calle Luna Calle Sol 13", UserType.User);
            await CheckUserAsync("1024", "Claudia", "Rojas", "claudia@yopmail.com", "322 311 4614", "Calle Luna Calle Sol 14", UserType.User);
            await CheckUserAsync("1025", "Diego", "Vega", "diego@yopmail.com", "322 311 4615", "Calle Luna Calle Sol 15", UserType.User);
            await CheckUserAsync("1026", "Verónica", "Cordero", "veronica@yopmail.com", "322 311 4616", "Calle Luna Calle Sol 16", UserType.User);
            await CheckUserAsync("1027", "Natalia", "Oliva", "natalia@yopmail.com", "322 311 4617", "Calle Luna Calle Sol 17", UserType.User);
            await CheckUserAsync("1028", "Felipe", "Sánchez", "felipe@yopmail.com", "322 311 4618", "Calle Luna Calle Sol 18", UserType.User);
            await CheckUserAsync("1029", "Lucía", "López", "lucia@yopmail.com", "322 311 4619", "Calle Luna Calle Sol 19", UserType.User);
            await CheckUserAsync("1030", "Fernando", "Bermúdez", "fernando@yopmail.com", "322 311 4620", "Calle Luna Calle Sol 20", UserType.User);
            await CheckUserAsync("1031", "Andres", "Morales", "andresmorales@yopmail.com", "322 311 4628", "Calle Luna Calle Sol 30", UserType.User);
            await CheckUserAsync("1032", "Ricardo", "Ramírez", "ricardo@yopmail.com", "322 311 4611", "Calle Luna Calle Sol 11", UserType.User);
        }

        private async Task CheckNoticesAsync()
        {
            if (!_context.Notices.Any())
            {
                await AddNoticeAsync("Elecciones 2024: Gana el presidente del verde",
                    "En las Elecciones de 2024, el candidato del Partido Verde emerge como ganador, asegurando la presidencia. Esta victoria marca un hito en la política nacional, ya que representa un cambio significativo en el panorama político del país. El resultado refleja el respaldo de la ciudadanía hacia las políticas y propuestas del partido, lo que promete influir en las futuras decisiones gubernamentales y en la dirección del país en los próximos años.",
                    1,
                    1,
                    new List<string>() { "GreenParty.png" });
                await AddNoticeAsync("Los 5 tips necesarios en su econonomía para salir de vacaciones",
                    "¿Planeas unas merecidas vacaciones pero te preocupa cómo manejar tus finanzas durante este tiempo? Descubre los 5 consejos clave para administrar tu economía mientras disfrutas de tus días libres. Desde establecer un presupuesto hasta buscar ofertas y promociones, estos tips te ayudarán a disfrutar al máximo sin descuidar tus finanzas.",
                    1,
                    11,
                    new List<string>() { "Holiday.png" });
                await AddNoticeAsync("La Atleta que Alcanzó la Cima: Historia de Superación y Éxito",
                    "Descubre la inspiradora historia de una atleta que superó todos los obstáculos para alcanzar el premio más alto en su disciplina. Desde enfrentar lesiones hasta sacrificios personales, esta atleta demostró determinación y perseverancia en su camino hacia la cima. Su historia es un ejemplo de cómo el esfuerzo y la dedicación pueden llevar al éxito más allá de las adversidades.",
                    1,
                    18,
                    new List<string>() { "Athlete.png" });
                await AddNoticeAsync("¡La Épica Aventura del 2024! Descubre la Película que Revolucionará el Cine!",
                    "Prepárate para una experiencia cinematográfica sin precedentes con la nueva película del 2024. Con una trama emocionante, efectos visuales impresionantes y un elenco estelar, esta película promete ser un hito en la historia del cine. Únete a los personajes en su épica travesía llena de acción, suspense y momentos inolvidables que te dejarán sin aliento. ¡No te pierdas este evento cinematográfico que marcará el inicio de una nueva era en el entretenimiento!",
                    1,
                    25,
                    new List<string>() { "Cinema.png" });
                await AddNoticeAsync("Descubre el Nuevo Dispositivo que Revoluciona tu Experiencia de Entretenimiento",
                    "¡Prepárate para elevar tu experiencia de entretenimiento al siguiente nivel con el innovador dispositivo del 2024! Diseñado para brindarte una inmersión total, este dispositivo te permite disfrutar de tus contenidos favoritos con una calidad nunca antes vista. Desde películas y juegos hasta música y más, este dispositivo transformará la manera en que experimentas el entretenimiento. ¡No te pierdas la oportunidad de ser parte de esta revolución tecnológica!",
                    1,
                    25,
                    new List<string>() { "Device.png" });
                await _context.SaveChangesAsync();
            }
        }

        private async Task AddNoticeAsync(string name, string description, int status, int categoryNoticeId, List<string> images)
        {
            Notice notice = new()
            {
                Name = name,
                Description = description,
                Status = status,
                CategoryNoticeId = categoryNoticeId,
                ImageNotices = new List<ImageNotice>()
            };

            foreach (string? image in images)
            {
                string filePath;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    filePath = $"{Environment.CurrentDirectory}\\Images\\notices\\{image}";
                }
                else
                {
                    filePath = $"{Environment.CurrentDirectory}/Images/notices/{image}";
                }
                var fileBytes = File.ReadAllBytes(filePath);
                var imagePath = await _fileStorage.SaveFileAsync(fileBytes, "jpg", "notices");
                notice.ImageNotices.Add(new ImageNotice { Name = image, File = imagePath, NoticeId = notice.Id });
            }

            _context.Notices.Add(notice);
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Leader.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Medellín");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var area = (userType == UserType.User)
                ? await _context.Areas.FirstOrDefaultAsync(x => x.Name == "Ingeniería")
                : null;

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                    Area = area
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);

                }

            return user;
        }

        private async Task CheckTypePermissionsAsync()
        {
            if (!_context.TypePermissions.Any())
            {
                _ = _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Vacaciones Pagadas",
                    Description = "Permiso para tomar vacaciones pagadas",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Vacaciones Anuales",
                            Description = "Vacaciones anuales concedidas a los empleados",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones Adicionales",
                            Description = "Vacaciones adicionales otorgadas por antigüedad o méritos especiales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones de Navidad",
                            Description = "Vacaciones concedidas durante la temporada de Navidad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones de Verano",
                            Description = "Vacaciones concedidas durante la temporada de verano",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones Familiares",
                            Description = "Vacaciones concedidas para pasar tiempo con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones de Emergencia",
                            Description = "Vacaciones concedidas para situaciones de emergencia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones de Cumpleaños",
                            Description = "Vacaciones concedidas para celebrar el cumpleaños del empleado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones por Logros",
                            Description = "Vacaciones concedidas por logros sobresalientes en el trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones por Enfermedad de un Familiar",
                            Description = "Vacaciones concedidas para cuidar a un familiar enfermo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Vacaciones por Viaje de Negocios",
                            Description = "Vacaciones concedidas para viajes de negocios relacionados con el trabajo",
                            Status = 1
                        },

                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Días de Enfermedad",
                    Description = "Permiso para tomar días de enfermedad remunerados",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Enfermedad Común",
                            Description = "Días de enfermedad remunerados para enfermedades comunes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Enfermedad Grave",
                            Description = "Días de enfermedad remunerados para enfermedades graves que requieren reposo prolongado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Enfermedad de un Familiar",
                            Description = "Días de enfermedad remunerados para cuidar a un familiar enfermo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Revisión Médica",
                            Description = "Días de enfermedad remunerados para realizar revisiones médicas preventivas",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Convalecencia",
                            Description = "Días de enfermedad remunerados para recuperarse después de una cirugía o enfermedad grave",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Día de Enfermedad Especial",
                            Description = "Días de enfermedad remunerados otorgados para cualquier razón de salud justificada",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado de un Niño Enfermo",
                            Description = "Días de enfermedad remunerados para cuidar a un niño enfermo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Tratamiento Médico",
                            Description = "Días de enfermedad remunerados para recibir tratamiento médico necesario",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Maternidad/Paternidad",
                            Description = "Días de enfermedad remunerados relacionados con la maternidad o paternidad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Acompañamiento a una Cita Médica",
                            Description = "Días de enfermedad remunerados para acompañar a un familiar a una cita médica",
                            Status = 1
                        },
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Maternidad/Paternidad",
                    Description = "Permiso remunerado para cuidado de maternidad/paternidad",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Licencia de Maternidad",
                            Description = "Permiso remunerado para cuidado de maternidad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia de Paternidad",
                            Description = "Permiso remunerado para cuidado de paternidad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Adopción",
                            Description = "Permiso remunerado para cuidado de un niño adoptado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Cuidado de Recién Nacido",
                            Description = "Permiso remunerado para cuidar a un recién nacido",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Cuidado de Niño Pequeño",
                            Description = "Permiso remunerado para cuidar a un niño pequeño",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Cuidado de Niño Enfermo",
                            Description = "Permiso remunerado para cuidar a un niño enfermo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Cuidado de Familiares Enfermos",
                            Description = "Permiso remunerado para cuidar a familiares enfermos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Tratamiento Médico de Familiares",
                            Description = "Permiso remunerado para acompañar a familiares en tratamientos médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Perdida de un Familiar",
                            Description = "Permiso remunerado para cuidar de asuntos relacionados con la perdida de un familiar",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Licencia por Complicaciones de Embarazo",
                            Description = "Permiso remunerado para cuidar de complicaciones relacionadas con el embarazo",
                            Status = 1
                        },
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Duelo",
                    Description = "Permiso remunerado para asistir a funerales o procesos de duelo",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Fallecimiento de Familiares",
                            Description = "Permiso para asistir a funerales de familiares directos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Fallecimiento de Amigos",
                            Description = "Permiso para asistir a funerales de amigos cercanos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Apoyo a Compañeros de Trabajo",
                            Description = "Permiso para asistir a funerales de compañeros de trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Procesos de Duelo",
                            Description = "Permiso para participar en procesos de duelo y acompañamiento",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Funerarios",
                            Description = "Permiso para realizar trámites relacionados con funerales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado de Familiares",
                            Description = "Permiso para cuidar de familiares en momentos de duelo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Conmemoraciones",
                            Description = "Permiso para participar en conmemoraciones relacionadas con el duelo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Apoyo Psicológico",
                            Description = "Permiso para recibir apoyo psicológico en momentos de duelo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Flexibilidad Horaria",
                            Description = "Permiso para tener flexibilidad horaria durante el proceso de duelo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Teletrabajo",
                            Description = "Permiso para realizar teletrabajo durante el proceso de duelo",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Motivos Personales",
                    Description = "Permiso remunerado para asuntos personales no relacionados con vacaciones o enfermedad",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Asuntos Familiares",
                            Description = "Permiso para atender asuntos relacionados con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Personales",
                            Description = "Permiso para atender asuntos personales no relacionados con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Eventos Especiales",
                            Description = "Permiso para asistir a eventos especiales personales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Personales",
                            Description = "Permiso para realizar trámites personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Celebraciones",
                            Description = "Permiso para participar en celebraciones personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Citas Médicas",
                            Description = "Permiso para asistir a citas médicas personales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Legales",
                            Description = "Permiso para atender asuntos legales personales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Financieros",
                            Description = "Permiso para atender asuntos financieros personales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Proyectos Personales",
                            Description = "Permiso para dedicarse a proyectos personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Descanso Personal",
                            Description = "Permiso para tomarse un día de descanso personal",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso sin Goce de Sueldo",
                    Description = "Permiso no remunerado para ausencia del trabajo",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Asuntos Personales",
                            Description = "Permiso para atender asuntos personales no relacionados con vacaciones o enfermedad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Familiares",
                            Description = "Permiso para atender asuntos relacionados con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Educación",
                            Description = "Permiso para asistir a eventos o actividades relacionadas con la educación",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Legales",
                            Description = "Permiso para atender trámites legales no relacionados con el trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Voluntariado",
                            Description = "Permiso para participar en actividades de voluntariado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado de Familiares",
                            Description = "Permiso para cuidar a familiares enfermos o necesitados",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Desarrollo Personal",
                            Description = "Permiso para actividades de desarrollo personal o profesional",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Emergencias",
                            Description = "Permiso para atender situaciones de emergencia familiar",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Proyectos Personales",
                            Description = "Permiso para dedicarse a proyectos personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Descanso Personal",
                            Description = "Permiso para tomarse un día de descanso personal",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Donación de Órganos",
                    Description = "Permiso remunerado para donación de órganos",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Asuntos Personales",
                            Description = "Permiso para atender asuntos personales no relacionados con vacaciones o enfermedad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Familiares",
                            Description = "Permiso para atender asuntos relacionados con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Educación",
                            Description = "Permiso para asistir a eventos o actividades relacionadas con la educación",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Legales",
                            Description = "Permiso para atender trámites legales no relacionados con el trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Voluntariado",
                            Description = "Permiso para participar en actividades de voluntariado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado de Familiares",
                            Description = "Permiso para cuidar a familiares enfermos o necesitados",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Desarrollo Personal",
                            Description = "Permiso para actividades de desarrollo personal o profesional",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Emergencias",
                            Description = "Permiso para atender situaciones de emergencia familiar",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Proyectos Personales",
                            Description = "Permiso para dedicarse a proyectos personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Descanso Personal",
                            Description = "Permiso para tomarse un día de descanso personal",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Servicio Militar",
                    Description = "Permiso remunerado para cumplir con servicio militar obligatorio",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Asuntos Personales",
                            Description = "Permiso para atender asuntos personales no relacionados con vacaciones o enfermedad",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Asuntos Familiares",
                            Description = "Permiso para atender asuntos relacionados con la familia",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Educación",
                            Description = "Permiso para asistir a eventos o actividades relacionadas con la educación",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Legales",
                            Description = "Permiso para atender trámites legales no relacionados con el trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Voluntariado",
                            Description = "Permiso para participar en actividades de voluntariado",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado de Familiares",
                            Description = "Permiso para cuidar a familiares enfermos o necesitados",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Desarrollo Personal",
                            Description = "Permiso para actividades de desarrollo personal o profesional",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Emergencias",
                            Description = "Permiso para atender situaciones de emergencia familiar",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Proyectos Personales",
                            Description = "Permiso para dedicarse a proyectos personales importantes",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Descanso Personal",
                            Description = "Permiso para tomarse un día de descanso personal",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Exámenes Médicos",
                    Description = "Permiso remunerado para asistir a exámenes médicos",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Consultas Médicas",
                            Description = "Permiso para asistir a consultas médicas programadas",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Exámenes de Laboratorio",
                            Description = "Permiso para realizarse exámenes de laboratorio",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Estudios Especializados",
                            Description = "Permiso para realizar estudios médicos especializados",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Tratamientos Médicos",
                            Description = "Permiso para recibir tratamientos médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Revisiones de Salud",
                            Description = "Permiso para revisiones médicas periódicas",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Exámenes Físicos",
                            Description = "Permiso para realizar exámenes físicos médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Diagnósticos Médicos",
                            Description = "Permiso para obtener diagnósticos médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Procedimientos Médicos",
                            Description = "Permiso para someterse a procedimientos médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Consulta con Especialistas",
                            Description = "Permiso para consultar con especialistas médicos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Cuidado Preventivo",
                            Description = "Permiso para recibir cuidado médico preventivo",
                            Status = 1
                        }
                    ]
                });
                _context.TypePermissions.Add(new TypePermission
                {
                    Name = "Permiso por Trámites Administrativos",
                    Description = "Permiso remunerado para realizar trámites administrativos",
                    Status = 1,
                    CategoryPermissions =
                    [
                        new CategoryPermission()
                        {
                            Name = "Trámites de Documentos Personales",
                            Description = "Permiso para realizar trámites relacionados con documentos personales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites de Vehículos",
                            Description = "Permiso para realizar trámites relacionados con vehículos",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Bancarios",
                            Description = "Permiso para realizar trámites bancarios",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Fiscales",
                            Description = "Permiso para realizar trámites fiscales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites de Vivienda",
                            Description = "Permiso para realizar trámites relacionados con vivienda",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Legales",
                            Description = "Permiso para realizar trámites legales",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites Laborales",
                            Description = "Permiso para realizar trámites relacionados con el trabajo",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites de Educación",
                            Description = "Permiso para realizar trámites relacionados con educación",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Trámites de Salud",
                            Description = "Permiso para realizar trámites relacionados con salud",
                            Status = 1
                        },
                        new CategoryPermission()
                        {
                            Name = "Otros Trámites",
                            Description = "Permiso para realizar otros trámites administrativos",
                            Status = 1
                        }
                    ]
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckNoticesAsync2()
        {
            if (!_context.Notices.Any())
            {
                _ = _context.Notices.Add(new Notice
                {
                    Name = "Elecciones 2024: Gana el presidente del verde",
                    Description = "En las Elecciones de 2024, el candidato del Partido Verde emerge como ganador, asegurando la presidencia. Esta victoria marca un hito en la política nacional, ya que representa un cambio significativo en el panorama político del país. El resultado refleja el respaldo de la ciudadanía hacia las políticas y propuestas del partido, lo que promete influir en las futuras decisiones gubernamentales y en la dirección del país en los próximos años.",
                    Status = 1,
                    CategoryNoticeId = 1,
                    ImageNotices =
                    [
                        new ImageNotice()
                        {
                            Name = "Imagen 1",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 2",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 3",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 4",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 5",
                        },
                    ]
                });
                _context.Notices.Add(new Notice
                {
                    Name = "Los 5 tips necesarios en su econonomía para salir de vacaciones",
                    Description = "¿Planeas unas merecidas vacaciones pero te preocupa cómo manejar tus finanzas durante este tiempo? Descubre los 5 consejos clave para administrar tu economía mientras disfrutas de tus días libres. Desde establecer un presupuesto hasta buscar ofertas y promociones, estos tips te ayudarán a disfrutar al máximo sin descuidar tus finanzas.",
                    Status = 1,
                    CategoryNoticeId = 11,
                    ImageNotices =
                    [
                        new ImageNotice()
                        {
                            Name = "Imagen 1",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 2",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 3",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 4",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 5",
                        },
                    ]
                });
                _context.Notices.Add(new Notice
                {
                    Name = "La Atleta que Alcanzó la Cima: Historia de Superación y Éxito",
                    Description = "Descubre la inspiradora historia de una atleta que superó todos los obstáculos para alcanzar el premio más alto en su disciplina. Desde enfrentar lesiones hasta sacrificios personales, esta atleta demostró determinación y perseverancia en su camino hacia la cima. Su historia es un ejemplo de cómo el esfuerzo y la dedicación pueden llevar al éxito más allá de las adversidades.",
                    Status = 1,
                    CategoryNoticeId = 18,
                    ImageNotices =
                    [
                        new ImageNotice()
                        {
                            Name = "Imagen 1",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 2",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 3",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 4",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 5",
                        },
                    ]
                });
                _context.Notices.Add(new Notice
                {
                    Name = "¡La Épica Aventura del 2024! Descubre la Película que Revolucionará el Cine",
                    Description = "Prepárate para una experiencia cinematográfica sin precedentes con la nueva película del 2024. Con una trama emocionante, efectos visuales impresionantes y un elenco estelar, esta película promete ser un hito en la historia del cine. Únete a los personajes en su épica travesía llena de acción, suspense y momentos inolvidables que te dejarán sin aliento. ¡No te pierdas este evento cinematográfico que marcará el inicio de una nueva era en el entretenimiento!",
                    Status = 1,
                    CategoryNoticeId = 25,
                    ImageNotices =
                    [
                        new ImageNotice()
                        {
                            Name = "Imagen 1",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 2",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 3",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 4",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 5",
                        },
                    ]
                });
                _context.Notices.Add(new Notice
                {
                    Name = "Descubre el Nuevo Dispositivo que Revoluciona tu Experiencia de Entretenimiento",
                    Description = "¡Prepárate para elevar tu experiencia de entretenimiento al siguiente nivel con el innovador dispositivo del 2024! Diseñado para brindarte una inmersión total, este dispositivo te permite disfrutar de tus contenidos favoritos con una calidad nunca antes vista. Desde películas y juegos hasta música y más, este dispositivo transformará la manera en que experimentas el entretenimiento. ¡No te pierdas la oportunidad de ser parte de esta revolución tecnológica!",
                    Status = 1,
                    CategoryNoticeId = 35,
                    ImageNotices =
                    [
                        new ImageNotice()
                        {
                            Name = "Imagen 1",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 2",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 3",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 4",
                        },
                        new ImageNotice()
                        {
                            Name = "Imagen 5",
                        },
                    ]
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckTypeNoticesAsync()
        {
            if (!_context.TypeNotices.Any())
            {
                _ = _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Política",
                    Description = "Noticias relacionadas con política",
                    Status = 1,
                    CategoryNotices =
                    [
                        new CategoryNotice()
                        {
                            Name = "Elecciones",
                            Description = "Noticias sobre elecciones y procesos electorales",
                            Status = 1
                        },
                        new CategoryNotice()
                        {
                            Name = "Gobierno",
                            Description = "Noticias sobre el gobierno y la administración pública",
                            Status = 1
                        },
                        new CategoryNotice()
                        {
                            Name = "Partidos Políticos",
                            Description = "Noticias sobre partidos políticos y movimientos sociales",
                            Status = 1
                        },
                        new CategoryNotice()
                        {
                            Name = "Legislación",
                            Description = "Noticias sobre leyes y legislación",
                            Status = 1
                        },
                        new CategoryNotice()
                        {
                            Name = "Opinión Política",
                            Description = "Artículos y opiniones sobre temas políticos",
                            Status = 1
                        },
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Economía",
                    Description = "Noticias relacionadas con economía",
                    Status = 1,
                    CategoryNotices =
                    [
                        new CategoryNotice
                        {
                            Name = "Finanzas",
                            Description = "Noticias sobre finanzas y mercados financieros",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Empleo",
                            Description = "Noticias sobre empleo y mercado laboral",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Comercio Internacional",
                            Description = "Noticias sobre comercio internacional y relaciones comerciales",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Crecimiento Económico",
                            Description = "Noticias sobre el crecimiento económico y desarrollo",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Inversiones",
                            Description = "Noticias sobre inversiones y gestión de activos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Impuestos",
                            Description = "Noticias sobre política fiscal y tributación",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Sector Empresarial",
                            Description = "Noticias sobre empresas y corporaciones",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Economía Digital",
                            Description = "Noticias sobre economía digital y tecnología financiera",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Desarrollo Económico",
                            Description = "Noticias sobre desarrollo económico y políticas públicas",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Deportes",
                    Description = "Noticias relacionadas con deportes",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Fútbol",
                            Description = "Noticias sobre fútbol y ligas de fútbol",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Baloncesto",
                            Description = "Noticias sobre baloncesto y ligas de baloncesto",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Tenis",
                            Description = "Noticias sobre tenis y torneos de tenis",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Atletismo",
                            Description = "Noticias sobre atletismo y eventos deportivos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes de Motor",
                            Description = "Noticias sobre deportes de motor y competiciones",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes Acuáticos",
                            Description = "Noticias sobre deportes acuáticos y eventos náuticos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes de Invierno",
                            Description = "Noticias sobre deportes de invierno y actividades en la nieve",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes Extremos",
                            Description = "Noticias sobre deportes extremos y aventuras al aire libre",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes de Equipo",
                            Description = "Noticias sobre deportes de equipo y competiciones colectivas",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Deportes Olímpicos",
                            Description = "Noticias sobre los Juegos Olímpicos y deportes olímpicos",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Entretenimiento",
                    Description = "Noticias relacionadas con entretenimiento",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Cine",
                            Description = "Noticias sobre películas, estrenos y la industria cinematográfica",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Música",
                            Description = "Noticias sobre música, artistas y conciertos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Televisión",
                            Description = "Noticias sobre programas de televisión y series",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Cultura Pop",
                            Description = "Noticias sobre cultura pop, celebridades y tendencias",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Arte",
                            Description = "Noticias sobre arte, exposiciones y eventos culturales",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Literatura",
                            Description = "Noticias sobre libros, escritores y eventos literarios",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Videojuegos",
                            Description = "Noticias sobre videojuegos, lanzamientos y la industria del gaming",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Humor",
                            Description = "Noticias sobre comedia, memes y contenido humorístico",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Eventos",
                            Description = "Noticias sobre eventos y espectáculos en vivo",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Celebridades",
                            Description = "Noticias sobre celebridades, chismes y rumores",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Ciencia y Tecnología",
                    Description = "Noticias relacionadas con ciencia y tecnología",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Tecnología",
                            Description = "Noticias sobre avances tecnológicos y gadgets",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Ciencia",
                            Description = "Noticias sobre descubrimientos científicos y avances en investigación",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Internet",
                            Description = "Noticias sobre internet, redes sociales y ciberseguridad",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Innovación",
                            Description = "Noticias sobre innovación y emprendimiento tecnológico",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Medicina",
                            Description = "Noticias sobre medicina, salud digital y biotecnología",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Espacio",
                            Description = "Noticias sobre exploración espacial y astronomía",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Inteligencia Artificial",
                            Description = "Noticias sobre inteligencia artificial y machine learning",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Robótica",
                            Description = "Noticias sobre robótica y automatización",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Energías Renovables",
                            Description = "Noticias sobre energías renovables y sostenibilidad",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Blockchain",
                            Description = "Noticias sobre blockchain y criptomonedas",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Salud",
                    Description = "Noticias relacionadas con salud",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Medicina",
                            Description = "Noticias sobre medicina, tratamientos y avances médicos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Salud Mental",
                            Description = "Noticias sobre salud mental, bienestar emocional y psicología",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Nutrición",
                            Description = "Noticias sobre alimentación saludable y dietas",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Fitness",
                            Description = "Noticias sobre ejercicio físico, entrenamiento y vida activa",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Enfermedades",
                            Description = "Noticias sobre enfermedades, prevención y tratamientos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Salud Infantil",
                            Description = "Noticias sobre salud infantil y cuidado de niños",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Salud Pública",
                            Description = "Noticias sobre políticas de salud, epidemias y pandemias",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Bienestar",
                            Description = "Noticias sobre bienestar general y calidad de vida",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Terapias Alternativas",
                            Description = "Noticias sobre terapias alternativas y medicina complementaria",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Investigación Médica",
                            Description = "Noticias sobre investigación médica y ensayos clínicos",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Cultura",
                    Description = "Noticias relacionadas con cultura",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Arte",
                            Description = "Noticias sobre arte, artistas y exposiciones",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Historia",
                            Description = "Noticias sobre historia, eventos históricos y personajes",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Música",
                            Description = "Noticias sobre música, conciertos y artistas",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Literatura",
                            Description = "Noticias sobre libros, escritores y eventos literarios",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Gastronomía",
                            Description = "Noticias sobre gastronomía, recetas y restaurantes",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Cine",
                            Description = "Noticias sobre películas, estrenos y festivales de cine",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Moda",
                            Description = "Noticias sobre moda, tendencias y diseñadores",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Religión",
                            Description = "Noticias sobre religión, creencias y prácticas espirituales",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Turismo",
                            Description = "Noticias sobre turismo, viajes y destinos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Festividades",
                            Description = "Noticias sobre festividades, celebraciones y eventos culturales",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Educación",
                    Description = "Noticias relacionadas con educación",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Ciencia",
                            Description = "Noticias sobre ciencia y descubrimientos científicos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Tecnología Educativa",
                            Description = "Noticias sobre tecnologías aplicadas a la educación",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Educación Superior",
                            Description = "Noticias sobre universidades, colegios y educación superior",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Educación Primaria",
                            Description = "Noticias sobre educación primaria y escuelas básicas",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Educación Secundaria",
                            Description = "Noticias sobre educación secundaria y colegios",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Pedagogía",
                            Description = "Noticias sobre pedagogía, métodos de enseñanza y aprendizaje",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Educación Inclusiva",
                            Description = "Noticias sobre educación inclusiva y diversidad en el aula",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Formación Profesional",
                            Description = "Noticias sobre formación profesional y educación técnica",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Educación a Distancia",
                            Description = "Noticias sobre educación a distancia y e-learning",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Políticas Educativas",
                            Description = "Noticias sobre políticas educativas y reformas en el sistema educativo",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Negocios",
                    Description = "Noticias relacionadas con negocios",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Finanzas",
                            Description = "Noticias sobre finanzas, mercados financieros y economía",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Emprendimiento",
                            Description = "Noticias sobre emprendimiento, startups y nuevas empresas",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Estrategia Empresarial",
                            Description = "Noticias sobre estrategias empresariales y management",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Marketing",
                            Description = "Noticias sobre marketing, publicidad y branding",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Comercio Internacional",
                            Description = "Noticias sobre comercio internacional y negocios globales",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Retail",
                            Description = "Noticias sobre retail, tiendas y tendencias de consumo",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Gestión de Recursos Humanos",
                            Description = "Noticias sobre gestión de recursos humanos y talento",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Inversiones",
                            Description = "Noticias sobre inversiones, fondos y bolsa de valores",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Negocios Sostenibles",
                            Description = "Noticias sobre negocios sostenibles y responsabilidad social corporativa",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Industria",
                            Description = "Noticias sobre industria, sectores y empresas",
                            Status = 1
                        }
                    ]
                });
                _context.TypeNotices.Add(new TypeNotice
                {
                    Name = "Medio Ambiente",
                    Description = "Noticias relacionadas con medio ambiente",
                    Status = 1,
                    CategoryNotices =
                     [
                        new CategoryNotice
                        {
                            Name = "Cambio Climático",
                            Description = "Noticias sobre cambio climático y calentamiento global",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Conservación",
                            Description = "Noticias sobre conservación de la naturaleza y biodiversidad",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Reciclaje",
                            Description = "Noticias sobre reciclaje, gestión de residuos y sostenibilidad",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Energías Renovables",
                            Description = "Noticias sobre energías renovables y transición energética",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Contaminación",
                            Description = "Noticias sobre contaminación ambiental y calidad del aire",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Biodiversidad",
                            Description = "Noticias sobre biodiversidad, especies en peligro y conservación",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Agua",
                            Description = "Noticias sobre gestión del agua y recursos hídricos",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Ecología",
                            Description = "Noticias sobre ecología, ecosistemas y medio ambiente",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Política Ambiental",
                            Description = "Noticias sobre políticas ambientales y legislación",
                            Status = 1
                        },
                        new CategoryNotice
                        {
                            Name = "Turismo Ecológico",
                            Description = "Noticias sobre turismo ecológico y viajes sostenibles",
                            Status = 1
                        }
                    ]
                });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckAreasAsync()
        {
            if (!_context.Areas.Any())
            {
                _context.Areas.Add(new Area { Name = "Talento Humano", Description = "Área de Talento Humano", Status = 1 });
                _context.Areas.Add(new Area { Name = "Administración", Description = "Área de Administración", Status = 1 });
                _context.Areas.Add(new Area { Name = "Ventas", Description = "Área de Ventas", Status = 1 });
                _context.Areas.Add(new Area { Name = "Marketing", Description = "Área de Marketing", Status = 1 });
                _context.Areas.Add(new Area { Name = "Desarrollo de Productos", Description = "Área de Desarrollo de Productos", Status = 1 });
                _context.Areas.Add(new Area { Name = "Servicio al Cliente", Description = "Área de Servicio al Cliente", Status = 1 });
                _context.Areas.Add(new Area { Name = "Contabilidad", Description = "Área de Contabilidad", Status = 1 });
                _context.Areas.Add(new Area { Name = "Tecnología de la Información", Description = "Área de Tecnología de la Información", Status = 1 });
                _context.Areas.Add(new Area { Name = "Logística", Description = "Área de Logística", Status = 1 });
                _context.Areas.Add(new Area { Name = "Producción", Description = "Área de Producción", Status = 1 });
                _context.Areas.Add(new Area { Name = "Ingeniería", Description = "Área de Ingeniería", Status = 1 });
                _context.Areas.Add(new Area { Name = "Finanzas", Description = "Área de Finanzas", Status = 1 });
                _context.Areas.Add(new Area { Name = "Legal", Description = "Área de Legal", Status = 1 });
                _context.Areas.Add(new Area { Name = "Comunicaciones", Description = "Área de Comunicaciones", Status = 1 });
                _context.Areas.Add(new Area { Name = "Calidad", Description = "Área de Calidad", Status = 1 });
                _context.Areas.Add(new Area { Name = "Investigación y Desarrollo", Description = "Área de Investigación y Desarrollo", Status = 1 });
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _ = _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States =
                    [
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = [
                                new() { Name = "Medellín" },
                                new() { Name = "Itagüí" },
                                new() { Name = "Envigado" },
                                new() { Name = "Bello" },
                                new() { Name = "Rionegro" },
                                new() { Name = "Marinilla" },
                            ]
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = [
                                new() { Name = "Usaquen" },
                                new() { Name = "Champinero" },
                                new() { Name = "Santa fe" },
                                new() { Name = "Useme" },
                                new() { Name = "Bosa" },
                            ]
                        },
                    ]
                });
                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States =
                    [
                        new State()
                        {
                            Name = "Florida",
                            Cities = [
                                new() { Name = "Orlando" },
                                new() { Name = "Miami" },
                                new() { Name = "Tampa" },
                                new() { Name = "Fort Lauderdale" },
                                new() { Name = "Key West" },
                            ]
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = [
                                new() { Name = "Houston" },
                                new() { Name = "San Antonio" },
                                new() { Name = "Dallas" },
                                new() { Name = "Austin" },
                                new() { Name = "El Paso" },
                            ]
                        },
                    ]
                });
            }
            await _context.SaveChangesAsync();
        }
    }
}