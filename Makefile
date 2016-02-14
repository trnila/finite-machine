finitemachine.exe:
	mcs finitemachine.cs
	
.PHONY: clean
clean:
	rm -f finitemachine.exe
