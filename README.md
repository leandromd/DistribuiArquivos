# DistribuiArquivos

**Descrição**
Função recursiva que "quebra" a pasta em várias outras pastas se existem muitos arquivos na pasta, ou se existirem arquivo muito grandes (conforme limites definidos).

Ou seja: cria novas pastas e distribui os arquivos para elas, conforme a limitação configurada de quantidade de arquivos por pastas e de tamanhos de arquivos (arquivos maiores do que o tamanho configurado ficarão sozinhos em pastas criadas só para eles).

**Parâmetros da function**
diretorioBase  =  a pasta que será a base para a "quebra"
diretorioAtual =  a pasta "da vez" da rodada da recursividade, ou seja, que foi criada na rodada anterior
quantDiretorios=  a quantidade de novas pastas que está sendo criada (que é o retorno recursivo da função)


**Configurações (dentro da function): limite de tamanho máximo por pasta e limite de quantidade de arquivos por pasta** 
(exemplos)
maxTamanho = (200 * 1000 * 1000); //200 MB
maxQuant   = 50;
