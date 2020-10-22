# Un juego de Plataformas
***El Reino Champiñón es un lugar hermoso y calmado, lleno de flores y como no, champiñones. Mario vive feliz en el reino, pero hace tiempo que todo ha canviado. En el reino se comenta que Bowser ha vuelto...***

¡Peach está en peligro! Lo que se comentaba en el reino es cierto, y Bowser la tiene encerrada en el castillo y ha enviado a sus secuaces Toad para impedir que llegues a él. Mario, recuerda, eres rápido y ágil, pero no olvides que los Toad son extremadamente venenosos, y un sólo toque con ellos será tu perdición. Por suerte, y como todos, tienen un punto débil:
Su cabeza es blanda y sensible, así que si caes sobre ellos, podrás eliminarlos.

Por suerte para ti el reino de Peach está lleno de items que te pueden ayudar, recoge las Super setas para sacar el Super Mario que llevas dentro. Y por supuesto no te dejes ninguna moneda atrás.

**¡Corre, el tiempo se agota y la princesa Peach necesita tu ayuda!**

## Reglas del Juego
El objetivo del juego es simple, llega donde se encuentra el castillo. Para conseguirlo podrás realizar dos acciones:
- Moverte: Usa las teclas flecha derecha y flecha izquierda para moverte a un lado u a otro respectivamente.
- Saltar: Usa la barra espaciadora para dar un salto.
### Fin de la partida
Mario puede perder por tres motivos:
- Caer al vacío por alguno de los acantilados que hay en el terreno.
- Mario toca a un **Toad** por cualquier punto de su cuerpo, a excepción de la cabeza. Si Mario cae sobre la cabeza de un Toad, será éste el que desaparezca.
- El cronómetro llega a 0.
### Super Mario
Si recoges una **Superseta** Mario va a crecer y se convertirá en Super Mario. Con estos poderes Mario puede romper bloques colgantes y obtener monedas. Además será capaz de resistir un golpe extra de **Toad** sin morir. El periodo de Super Mario es limitado, así que no pierdas el tiempo.
### Cajas sorpresa
Si Mario sacude una caja con la cabeza obtendrá una recompensa. Hay dos tipos de recompensas:
- Super setas: Dan a Mario un poder increíble.
- Monedas: Gana puntos y son acumulables.
## Estuctura del Proyecto
### Escena y recursos
La escena se ha construído usando el sistema de **tilemap**.
Se han usado recursos gráficos y sonoros que se han obtenido de las siguientes fuentes:
- Sprites: 
- - https://www.spriters-resource.com/nes/supermariobros/
- - http://www.mariouniverse.com/sprites-nes-smb/
- Sonidos:
- - https://themushroomkingdom.net/media/smb/wav
- Fuentes:
- - http://www.superluigibros.com/mario-bros-fonts
### Assets
La estrucutra de los Assets contiene los siguientes elementos:
- **Animations:** Contiene todas las animaciones de los personajes creadas por Unity.
- **Fonts:** Contiene las fuentes que se usan para los elementos de UI.
- **Prefabs:** Elementos que se repiten en el juego. Algunos de ellos se instancian directamente mediante scripts, como es el caso de los items.
- **Resources:** Contiene principalmente los sonidos que se cargan en su mayoria mediante scripts.
- **Scenes:** Contiene una única escena.
- **Scripts:** Mantiene todos los scripts del proyecto. Leer más adelante para saber más.
- **Sprites:** Contiene todos los Sprites tratados y convertidos en multi sprites.
#### Scripts
Los scripts controlan diferentes funcionalidades de los componentes. Estos son las principales funciones de cada uno.
- **GamePlayManager:** Gestiona las acciones principales del juego, como reiniciar la partida o finalizarla. También gestiona como se muestra la información en el UI del juego y los audios principales (melodia principal, game over y objetivo cumplido).
- **MarioController:** Tiene la responsabilidad de todo lo que se relaciona con el personaje de Mario. Controla los movimientos (andar y saltar) así como las animaciones que se producen en los diferentes estados o la transformación de Mario en Super Mario. Este script es el encargado también de finalizar el juego ya sea por muerte o victoria, mediante los colliders que interaccionan con Mario.
- **ToadController:** Controla el comportamiento de los Toad. Principalmente permite el movimiento, de un lado a otro, cambiando de dirección cuando el Toad colisiona con algún elemento. También gestiona las colisiones en la cabeza, lo que mata al Toad.
- **GoldBoxController:** Se responsabiliza del comportamiento de las cajas sorpresa. Principalmente detecta si entra en contacto con la cabeza de Mario para así instanciar el objecto que tenga asignado (Super setas o monedas). Aplica la animación correspondiente.
- **ItemController:** Aplica el comportamiento asignado tanto para **super setas** como para las **monedas**. Los parámetros que recibe le permite ejecutar el sonido correspondiente y además se encarga de aplicar la recompensa al recoger el objeto y eliminarlo.
- **MarioFollower:** Controla el movimiento de la cámara. Sólo produce movimiento horizontal y se detiene cuando detecta que Mario se aproxima a los bordes del escenario.
- **BlockController:** Destruye los bloques si Super Mario colisiona con ellos con la cabeza. Muestra una moneda si esto sucede.
- **WinZone / DeadZone:** Tiene comportamientos parecidos. Se encargan de detectar cuando Mario entra en su zona y ejecutar un reinicio o fin del juego dependiendo si es zona de muerte o ganadora.
- **CounterManager:** Esta clase estática contabiliza los puntos y monedas acumuladas. Todos los elementos del juego tienen acceso a sus métodos y así añadir puntos o monedas en el contador cuando se colisiona con ellos (matar un Toad, romper un bloque, revelar el item oculto en las cajas, etc...).

## Preview
[Ver archivo en mp4](https://gitlab.com/Somo86/albertsomoza-pec2/-/blob/master/un_juego_de_plataformas.mp4)


