﻿SELECT 
    assunto,
    ano,
    COUNT(*) as quantidade
FROM atendimentos
GROUP BY assunto, ano
HAVING COUNT(*) > 3
ORDER BY ano DESC, quantidade DESC;