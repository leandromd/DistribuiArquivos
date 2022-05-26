

{
    public class DistribuiArquivos
    {

        private int DistribuiArquivos(string diretorio)
        {
            return DistribuiArquivos(diretorio, diretorio, 0);
        }

        /// <summary>
        /// função recursiva que "quebra" a pasta em várias outras pastas se existem muitos arquivos na pasta, ou se existirem arquivo muito grandes
        /// ou seja: cria novas pastas e distribui os arquivos para elas, conforme a limitação configurada de quantidade de arquivos por pastas e 
        /// de tamanhos de arquivos (arquivos maiores do que o tamanho configurado ficarão sozinhos em pastas criadas só para eles)
        /// </summary>
        /// <param name="diretorioBase"></param> a pasta que será a base para a "quebra"
        /// <param name="diretorioAtual"></param> a pasta "da vez" da rodada da recursividade, ou seja, que foi criada na rodada anterior
        /// <param name="quantDiretorios"></param> a quantidade de novas pastas que está sendo criada (que é o retorno recursivo da função)
        /// <returns></returns>
        private int DistribuiArquivos(string diretorioBase, string diretorioAtual, int quantDiretorios)
        {
            string novoDiretorio = string.Empty;

            const Int64 maxTamanho = (200 * 1000 * 1000); //200 MB
            const Int64 maxQuant = 50;
            Int64 tamanho = 0;
            int quant = 0;
            string[] arquivos = Directory.GetFiles(diretorioAtual);
            foreach (string arq in arquivos)
            {
                FileInfo infoArquivo = new FileInfo(arq);
                tamanho += infoArquivo.Length;
                quant++;

                if (tamanho > maxTamanho || quant > maxQuant) //se o total de arquivos da pasta ultrapassar o tamanho máximo em MB ou se existirem mais arquivos do que o o máximo na mesma pasta...
                {
                    //se o arquivo da vez sozinho ultrapassar o tamanho máximo, ele já movido agora para uma pasta só para ele -- e na próxima recursividade, esta pasta é "pulada"
                    if (infoArquivo.Length > maxTamanho)
                    {
                        quantDiretorios++;
                        CriaDiretorio($"{diretorioBase}___{quantDiretorios + 1}");
                        File.Move(infoArquivo.FullName, Path.Combine($"{diretorioBase}___{quantDiretorios + 1}", infoArquivo.Name));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(novoDiretorio)) //... cria uma nova pasta (se ela ainda não foi criada no loop)...
                        {
                            novoDiretorio = $"{diretorioBase}___{quantDiretorios + 1}";
                            CriaDiretorio(novoDiretorio);
                        }
                        else
                        {
                            //...e move os arquivos restantes da pasta original (a partir do próximo em que foi identificado o "corte") para a nova pasta.
                            File.Move(infoArquivo.FullName, Path.Combine(novoDiretorio, infoArquivo.Name));
                        }
                    }
                }
            }

            //se houve a necessidade de criar um nova pasta, faz a mesma verificação (chama esta function de forma recursiva)
            if (!string.IsNullOrEmpty(novoDiretorio))
            {
                quantDiretorios = DistribuiArquivos(diretorioBase, novoDiretorio, quantDiretorios + 1);
            }

            return quantDiretorios;
        }
   
    }

}   
