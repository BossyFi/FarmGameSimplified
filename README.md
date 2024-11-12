# FarmGame
Este será el GDD del proyecto FarmGame (nombre por confirmar) del proyecto realizado entre:
- Bozhidar Petrov Valchev
- Miguel Vidal De La Plaza

# ¿De qué va FarmGame?

Juego sencillo de cuidado y atención de animales divididos por biomas. El cuidado de los animales se basará en un sistema tipo "Tamagotchi" donde habrá que consultar periódicamente el estado de los animales y llevar a cabo los cuidados necesarios (alimentarlos, limpiarlos, jugar con ellos...). El objetivo del juego consiste en desbloquear todos los biomas disponibles accediendo así a más animales.
# Guión

El juego se contextualizará dentro de un mundo habitado por BLOPYS, seres se otro planeta con una sociedad avanzada. Dentro de este contexto encarnaremos al "coleccionista", un Blopy cuyo objetivo es construir una reserva natural de animales terrestres donde poder interactuar con ellos y permitir que el resto de su especie los conozca y se interese también por ellos.
# Mecánicas

Las mecánicas del juego se centrarán por un lado en los animales de forma individual y por otro lado en el bioma de forma global. El objetivo de estas mecánicas consiste en aumentar la felicidad de los animales, la cual tendrá un efecto directamente proporcional en la inversión realizada por los visitantes de la reserva natural - la economía de la RN se explicará en el apartado "Reserva Natural" -.

## Animales

El nivel de felicidad de los animales dependerá de sus estado, el cual variará en función de los siguientes factores - todos definidos con un valor entre 0 y 1- :

 - **Hambre**: El hambre de los animales descenderá gradualmente conforme pase el tiempo (una vez transcurrido un tiempo dado tras la ingesta), su efecto sobre la felicidad funcionara mediante una curva de animación definida en Unity que aumentará exponencialmente conforme el hambre disminuya (una vez superado el umbral del 0.75).<br />
 Para aumentar el valor de esta variable de estado se deberá **ALIMENTAR** al animal con los alimentos disponibles en la tienda -explicada en el apartado "Reserva Natural"-, de esta forma la variable regresará a su valor máximo de 1. Alimentar a los animales mejorará tu vínculo de amistad con ellos - ver apartado "Sistema de amistad" - y se podrá conseguir un multiplicador en este apartado si además se les alimenta con su comida favorita.

 - **Aseo**: El aseo del animal disminurá gradualmente con el paso del tiempo transcurrido un tiempo tras su último lavado. El efecto de este en su felicidad será directamente proporcional al mismo.<br />
 Para aumentar el valor de esta variable se deberá **LIMPIAR** al animal, lo cual devolverá a la variable a su valor máximo de 1. Limpiar a los animales mejorará tu vínculo de amistad con ellos.

 - **Entretenimiento**: El entretenimiento de los animales disminuirá con el paso del tiempo de forma constante, su efecto sobre la felicidad estará definido mediante una curva de animación definida en Unity que aumentará de forma inversamente proporcional a su valor.<br />
 Para aumentar el valor de esta variable se deberá **JUGAR** con el animal, esta acción devolverá la variable a un valor de 1. Jugar con los animales mejorará tu vínculo de amistad con ellos.

**Nota**: la velocidad con la que varían estas variables variará en función de los distintos animales.
### Sistema de amistad

Al interactuar con los animales aumentará tu **VINCULO** con ellos, este vínculo determinará tu relación con el animal y afectará directamente a los ingresos obtenidos por los visitatnes.<br />

**Mecánica de compañero**: Una vez alcanzado un nivel de amistad de 3 con un animal, podrás establecer a este animal como tu **COMPAÑERO**, lo cual hará que te siga por el entorno y que su nivel de amistad aumente ligeramente cuando interactues con otros animales también.

## Reserva Natural

La reserva natural constará de diversos biomas que contendrán a los distintos animales. Cada cierto tiempo la RN será visitada por Blopys, los cuales pasearán libremente por el entorno, tras finalizar su visita los blopys se marcharán dejando una **DONACIÓN** en forma de mondeda local, que podrás utilizar para mejorar la RN.<br />

El cantidad de dinero donado variará en función de la felicidad y la amistad con los animales, a mayor la amistad y la felicidad, mayor la donación. Por lo tanto el objetivo del jugador consistirá en cuidar de los animales para maximizar eta recompensa.<br />

Además, los biomas tendrán un estado que afectará directamente a la felicidad de los animales mediante un multiplicador. El **ESTADO DEL BIOMA** se medirá con los siguientes indicadores:

 - **Limpieza**: Variará en función de la cantidad de heces de animales que haya desperdigadas por el entorno. Para mejorar esta variable sencillamente se deberán **LIMPIAR** las heces de los animales.
  
 - **Estilo**: Variable que aumentará en función de los objetos dispuestos en el bioma. Estos objetos se podrán obtener en la tienda - leer apartado siguiente - y tendran un valor proporcional a los puntos de estilo aportados.

### Tienda

La reserva natural constará de una tienda con 3 apartados:
 - **Comida**: Donde se podrá obtener comida para los distintos animales.
 - **Decoración**: Donde se podrán obtener los distintos elementos de decoración para mejorar el estilo del bioma.
 - **Animales/Bioma**: **POR DEFINIR** la idea es introducir un método de obtención de nuevos animales y biomas.


# Ideas

##  Cuidadores

Una vez avanzado el juego, es decir, cuando se tenga un número elevado de animalitos vas poder contratar otros blopys denominados **CUIDADORES**. Estos blopys podrán encargarse de un número determinado de tareas que pasarán a estar automatizadas. Estas tareas serán asignadas por el usuario y cada una ocupará un slot de las posibles tareas asignables a un cuidador:
 - **Alimentar animales**
 - **Limpiar animales**
 - **Jugar con animales**
 - **Limpiar biomas** (esta tarea convertirá al blopy en un limpiador y no se le podrán asignar más tareas)
